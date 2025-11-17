using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data
{
    public class UserReceiptDto
    {
        public int ReceiptId { get; set; }
        public string Message { get; set; }
        public string FormattedAmount { get; set; }
    }
}
