using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        public string LogId { get; set; }
        public string UserId { get; set; }

        // 1. Total Amount (Pehle kitne thay)
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OpeningBalance { get; set; } 

        // 2. Kitne Transfer huay
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransferredAmount { get; set; } 

        // 3. Bachay kitne (Closing)
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ClosingBalance { get; set; } 

        public string TransactionType { get; set; } // Debit/Credit

        public string SecretPin { get; set; } // Ye ab Encrypted hoga
        public bool IsSuccess { get; set; }


    }
}
