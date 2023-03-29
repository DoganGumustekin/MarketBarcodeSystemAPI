namespace MarketBarcodeSystemAPI.Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; } //anahtar değeri
        public DateTime Expiration { get; set; } //o tokenin bitiş süresi
    }
}
