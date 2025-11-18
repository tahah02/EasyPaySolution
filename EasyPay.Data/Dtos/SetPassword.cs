using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dtos
{
    public class SetPassword
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
