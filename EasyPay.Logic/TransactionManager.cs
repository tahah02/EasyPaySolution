using EasyPay.Data;
using EasyPay.Data.Dtos;
using EasyPay.Data.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyPay.Logic
{
    public class TransactionManager : ITransactionManager
    {
        private readonly EasyPayDbContext _context;

        public TransactionManager(EasyPayDbContext context)
        {
            _context = context;
        }

        // Helper to generate ID
        private string GenerateLogId()
        {
            return "TRX-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        // 1. GET HISTORY
        public ApiResponse<List<UserReceiptDto>> GetUserHistory(string userId)
        {
            string traceId = GenerateLogId(); // Tracking ID for this view action

            var history = _context.PaymentRecords
                .Where(x => x.UserId == userId)
                .Select(x => new UserReceiptDto
                {
                    ReceiptId = x.Id,
                    Message = x.IsSuccess ? "Success" : "Failed",
                    FormattedAmount = $"Rs. {x.TransferredAmount} ({x.TransactionType})"
                })
                .ToList();

            // Dhabbe mein pack karke bhejo
            return new ApiResponse<List<UserReceiptDto>>
            {
                LogId = traceId,
                IsSuccess = true,
                Message = "History Fetched Successfully",
                Data = history
            };
        }

        // 2. ADD TRANSACTION (Manual)
        public ApiResponse<string> AddTransaction(PaymentRequestDto request)
        {
            string logId = GenerateLogId(); // Generate Unique ID

            var newRecord = new PaymentRecord
            {
                LogId = logId, // Save to DB
                UserId = request.UserId,
                TransferredAmount = request.Amount,
                TransactionType = "Manual",
                SecretPin = SecurityHelper.Encrypt(request.SecretPin),
                IsSuccess = request.IsSuccess
            };
            _context.PaymentRecords.Add(newRecord);
            _context.SaveChanges();

            return new ApiResponse<string>
            {
                LogId = logId,
                IsSuccess = true,
                Message = "Transaction Added Manually",
                Data = "Saved in Database"
            };
        }

        // 3. GET BALANCE
        public ApiResponse<decimal> GetBalance(string userId)
        {
            string traceId = GenerateLogId(); // Tracking ID
            var user = _context.UserAccounts.FirstOrDefault(u => u.UserId == userId);

            decimal balance = user == null ? 0 : user.CurrentBalance;

            return new ApiResponse<decimal>
            {
                LogId = traceId,
                IsSuccess = true,
                Message = "Balance Fetched",
                Data = balance
            };
        }

        // 4. TRANSFER MONEY
        public ApiResponse<string> TransferMoney(TransferRequestDto request)
        {
            // 1. ID Generate ki (e.g. TRX-AAA)
            string logId = GenerateLogId();

            var sender = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.FromUser);
            var receiver = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.ToUser);

            if (sender == null || receiver == null)
                return new ApiResponse<string> { LogId = logId, IsSuccess = false, Message = "User Not Found", Data = null };

            if (sender.CurrentBalance < request.Amount)
                return new ApiResponse<string> { LogId = logId, IsSuccess = false, Message = "Insufficient Balance", Data = null };

            // Logic
            decimal senderOpening = sender.CurrentBalance;
            decimal receiverOpening = receiver.CurrentBalance;
            sender.CurrentBalance -= request.Amount;
            receiver.CurrentBalance += request.Amount;

            var senderRecord = new PaymentRecord
            {
                LogId = logId, // FIX: Upar wali variable use ki (Same ID rahegi)
                UserId = request.FromUser,
                OpeningBalance = senderOpening,
                TransferredAmount = request.Amount,
                ClosingBalance = sender.CurrentBalance,
                TransactionType = "Debit",
                SecretPin = SecurityHelper.Encrypt("SENT"),
                IsSuccess = true
            };

            var receiverRecord = new PaymentRecord
            {
                LogId = logId, // ✅ FIX: Same ID
                UserId = request.ToUser,
                OpeningBalance = receiverOpening,
                TransferredAmount = request.Amount,
                ClosingBalance = receiver.CurrentBalance,
                TransactionType = "Credit",
                SecretPin = SecurityHelper.Encrypt("RECEIVED"),
                IsSuccess = true
            };

            _context.PaymentRecords.Add(senderRecord);
            _context.PaymentRecords.Add(receiverRecord);
            _context.SaveChanges();

            return new ApiResponse<string>
            {
                LogId = logId,
                IsSuccess = true,
                Message = "Transfer Successful",
                Data = $"Amount {request.Amount} transferred"
            };
        }
        public ApiResponse<string> SetPassword(SetPasswordDto request)
        {
            string logId = "PWD-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            // 1. User dhoondo
            var user = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.UserId);

            if (user == null)
            {
                return new ApiResponse<string>
                {
                    LogId = logId,
                    IsSuccess = false,
                    Message = "User nahi mila!",
                    Data = null
                };
            }

            // 2. Password ko Hash karo (Kachumar nikalo)
            string hashedPassword = SecurityHelper.HashPassword(request.NewPassword);

            // 3. Database mein Hash save karo
            user.PasswordHash = hashedPassword;

            _context.SaveChanges();

            return new ApiResponse<string>
            {
                LogId = logId,
                IsSuccess = true,
                Message = "Password successfully set hua!",
                Data = "Password Secured & Hashed"
            };
        }
    }
}