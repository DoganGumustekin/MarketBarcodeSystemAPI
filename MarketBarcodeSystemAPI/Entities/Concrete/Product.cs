using MarketBarcodeSystemAPI.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace MarketBarcodeSystemAPI.Entities.Concrete
{
    public class Product : IEntity
    {
        [Key]
        public long BarcodeId { get; set; }
        public int AccountKey { get; set; }
        public byte[]? ImageData { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}
