namespace MarketBarcodeSystemAPI.Core.Entities.Concrete
{
    public class User:IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswortSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool isWorkMan { get; set; }
        public bool Status { get; set; } //durum 
    }

    public class UserListModel:IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool isWorkMan { get; set; }
        public bool Status { get; set; } //durum 
    }
}
