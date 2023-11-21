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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LabelPricingDb;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
    }
}
