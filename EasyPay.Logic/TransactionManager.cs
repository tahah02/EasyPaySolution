//using EasyPay.Data;
//using EasyPay.Data.Dtos;
//using System.Collections.Generic;
//using System.Linq;

//namespace EasyPay.Logic
//{
//    public class TransactionManager : ITransactionManager
//    {
//        private readonly AppDbContext _context;

//        public TransactionManager(AppDbContext context)
//        {
//            _context = context;
//        }

//        // 1. GET HISTORY (Updated for new columns)
//        public List<UserReceiptDto> GetUserHistory(string userId)
//        {
//            return _context.PaymentRecords
//                .Where(x => x.UserId == userId)
//                .Select(x => new UserReceiptDto
//                {
//                    ReceiptId = x.Id,
//                    Message = x.IsSuccess ? "Success" : "Failed",
//                    // Note: Ab 'Amount' nahi, 'TransferredAmount' use hoga
//                    FormattedAmount = $"Rs. {x.TransferredAmount} ({x.TransactionType})"
//                })
//                .ToList();
//        }

//        // 2. ADD TRANSACTION (Updated for new columns)
//        public void AddTransaction(PaymentRequestDto request)
//        {
//            var newRecord = new PaymentRecord
//            {
//                UserId = request.UserId,
//                TransferredAmount = request.Amount, // Name change hua
//                OpeningBalance = 0, // Manual add mein hum 0 rakh rahe hain filhal
//                ClosingBalance = 0,
//                TransactionType = "Manual",
//                SecretPin = SecurityHelper.Encrypt(request.SecretPin), // Encrypt kar diya
//                IsSuccess = request.IsSuccess
//            };
//            _context.PaymentRecords.Add(newRecord);
//            _context.SaveChanges();
//        }

//        // 3. GET BALANCE
//        public decimal GetBalance(string userId)
//        {
//            var user = _context.UserAccounts.FirstOrDefault(u => u.UserId == userId);
//            return user == null ? 0 : user.CurrentBalance;
//        }

//        // 4. TRANSFER MONEY (Professional Ledger Logic)
//        public string TransferMoney(TransferRequestDto request)
//        {
//            var sender = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.FromUser);
//            var receiver = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.ToUser);

//            if (sender == null || receiver == null) return "User nahi mila";
//            if (sender.CurrentBalance < request.Amount) return "Balance kam hai";

//            string transactionRef = "TRX-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

//            // Calculations
//            decimal senderOpening = sender.CurrentBalance;
//            decimal receiverOpening = receiver.CurrentBalance;

//            sender.CurrentBalance -= request.Amount;
//            receiver.CurrentBalance += request.Amount;

//            // Sender Record (Debit)
//            var senderRecord = new PaymentRecord
//            {
//                UserId = request.FromUser,
//                OpeningBalance = senderOpening,
//                TransferredAmount = request.Amount,
//                ClosingBalance = sender.CurrentBalance,
//                TransactionType = "Debit",
//                SecretPin = SecurityHelper.Encrypt(request.FromUser == "Ayesha" ? "1234" : "SENT"),
//                IsSuccess = true
//            };

//            // Receiver Record (Credit)
//            var receiverRecord = new PaymentRecord
//            {
//                UserId = request.ToUser,
//                OpeningBalance = receiverOpening,
//                TransferredAmount = request.Amount,
//                ClosingBalance = receiver.CurrentBalance,
//                TransactionType = "Credit",
//                SecretPin = SecurityHelper.Encrypt("RECEIVED"),
//                IsSuccess = true
//            };

//            _context.PaymentRecords.Add(senderRecord);
//            _context.PaymentRecords.Add(receiverRecord);
//            _context.SaveChanges();

//            return "Transfer Successful with Full Ledger!";
//        }

//    }
//}
using EasyPay.Data;
using EasyPay.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyPay.Logic
{
    public class TransactionManager : ITransactionManager
    {
        private readonly AppDbContext _context;

        public TransactionManager(AppDbContext context)
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
            string logId = GenerateLogId(); // Ye ID DB mein bhi jayegi aur User ko bhi milegi

            var sender = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.FromUser);
            var receiver = _context.UserAccounts.FirstOrDefault(u => u.UserId == request.ToUser);

            // Validations
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
                LogId = "TRX-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
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
                LogId = logId,
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
    }
}