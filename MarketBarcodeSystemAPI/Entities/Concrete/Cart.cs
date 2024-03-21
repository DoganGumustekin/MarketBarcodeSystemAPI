using MarketBarcodeSystemAPI.Core.Entities;

namespace MarketBarcodeSystemAPI.Entities.Concrete
{
    public class Cart:IEntity
    {
        public long CartId { get; set; }
        public int UserId { get; set; }
        public long BarcodeId { get; set; }
        public int NumberOfProduct { get; set; }
    }

    public class CartForOrderModel
    {
        public long CartId { get; set; }
        public int UserId { get; set; }
        public long BarcodeId { get; set; }
        public int AccountId { get; set; }
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public int NumberOfProduct { get; set; }

    }
}
