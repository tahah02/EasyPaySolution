using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string AccountTitle { get; set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrentBalance { get; set; } 
    }
}
