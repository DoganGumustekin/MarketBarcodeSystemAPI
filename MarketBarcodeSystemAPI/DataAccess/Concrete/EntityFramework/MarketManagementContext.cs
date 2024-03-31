using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework
{
    public class MarketManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LabelPricingDb;Trusted_Connection=true");
            optionsBuilder.UseSqlServer(@"Server=sql.bsite.net\MSSQL2016;Database=marketsystem_;User Id=marketsystem_; password=marketsystem.!;Trusted_Connection=false;Encrypt=false;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
