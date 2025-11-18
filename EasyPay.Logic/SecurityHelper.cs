using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Logic
{
    public static class SecurityHelper
    {
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Password ko bytes mein badla
                var bytes = Encoding.UTF8.GetBytes(password);
                // Hash kiya
                var hash = sha256.ComputeHash(bytes);
                // Wapas string bana diya (Hex format mein)
                return Convert.ToBase64String(hash);
            }
        }
    }
}
