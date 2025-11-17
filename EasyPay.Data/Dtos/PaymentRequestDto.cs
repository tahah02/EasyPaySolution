using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dtos
{
    public class PaymentRequestDto
    {
        public string LogId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string SecretPin { get; set; }
        public bool IsSuccess { get; set; }
    }
}
