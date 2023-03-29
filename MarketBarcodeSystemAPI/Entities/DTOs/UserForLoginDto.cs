using MarketBarcodeSystemAPI.Core.Entities;

namespace MarketBarcodeSystemAPI.Entities.DTOs
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
