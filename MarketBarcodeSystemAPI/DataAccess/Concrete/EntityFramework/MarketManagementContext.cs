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
    }
}
