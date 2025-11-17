using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {       }

        // Is line se SQL mein "PaymentRecords" naam ka Table banega
        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
