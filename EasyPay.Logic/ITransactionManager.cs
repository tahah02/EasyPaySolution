using EasyPay.Data;
using EasyPay.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Logic
{
    public interface ITransactionManager
    {
        ApiResponse<List<UserReceiptDto>> GetUserHistory(string userId);
        ApiResponse<string> AddTransaction(PaymentRequestDto request);
        ApiResponse<decimal> GetBalance(string userId);
        ApiResponse<string> TransferMoney(TransferRequestDto request);
        ApiResponse<string> SetPassword(SetPasswordDto request);
    }
}
