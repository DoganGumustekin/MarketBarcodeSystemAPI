using MarketBarcodeSystemAPI.Core.Entities;

namespace MarketBarcodeSystemAPI.Entities.Concrete
{
    public class Order : IEntity
    {
        public long OrderId { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public long BarcodeId { get; set; }
        public int AccountId { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int NumberOfProduct { get; set; }
        public double TotalPrice { get; set; }
    }
}
