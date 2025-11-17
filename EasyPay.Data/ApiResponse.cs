using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data
{
    public class ApiResponse<T>
    {
        public string LogId { get; set; }      // Tracking ID (Unique)
        public string Message { get; set; }    // "Success" ya "Error"
        public T Data { get; set; }            // Asli maal (Balance, History, etc.)
        public bool IsSuccess { get; set; }
    }
}
