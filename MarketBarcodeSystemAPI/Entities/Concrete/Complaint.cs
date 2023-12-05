using MarketBarcodeSystemAPI.Core.Entities;

namespace MarketBarcodeSystemAPI.Entities.Concrete
{
    public class Complaint:IEntity
    {
        public int ComplaintId { get; set; }
        public int AccountId { get; set; }
        public long BarcodeId { get; set; }
        public int UserId { get; set; }
        public string? ComplaintDescription { get; set; }
        public bool isChecked { get; set; }
        public DateTime ComplaintDate { get; set; } = DateTime.Now;
    }

    public class ComplaintForManagerModel
    {
        public int ComplaintId { get; set; }
        public int AccountId { get; set; }
        public long BarcodeId { get; set; }
        public int UserId { get; set; }
        public string? ProductName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ComplaintDescription { get; set; }
        public bool isChecked { get; set; }
        public DateTime ComplaintDate { get; set; }
    }
}
