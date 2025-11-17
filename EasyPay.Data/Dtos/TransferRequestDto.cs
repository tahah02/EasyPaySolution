using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dtos
{
    public class TransferRequestDto
    {
        public string FromUser { get; set; } // Paisa bhejane wala
        public string ToUser { get; set; }   // Paisa lene wala
        public decimal Amount { get; set; }
    }
}
