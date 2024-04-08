using MarketBarcodeSystemAPI.Core.Entities;

namespace MarketBarcodeSystemAPI.Entities.Concrete
{
    public class Account:IEntity
    {
        public int AccountId { get; set; }
        public int AccountKey { get; set; }
        public int UserId { get; set; }
        public string? AccountName { get; set; }
    }
}
