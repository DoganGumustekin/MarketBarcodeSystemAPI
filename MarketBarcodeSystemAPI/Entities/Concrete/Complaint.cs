using MarketBarcodeSystemAPI.Core.Entities;

namespace MarketBarcodeSystemAPI.Entities.Concrete
{
    public class Complaint:IEntity
    {
        public int ComplaintId { get; set; }
        public long BarcodeId { get; set; }
        public int UserId { get; set; }
        public string? ComplaintDescription { get; set; }
        public bool isChecked { get; set; }
        public DateTime ComplaintDate { get; set; }
    }
}
