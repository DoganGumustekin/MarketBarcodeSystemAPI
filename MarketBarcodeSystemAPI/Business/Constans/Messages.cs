using MarketBarcodeSystemAPI.Core.Entities.Concrete;

namespace MarketBarcodeSystemAPI.Business.Constans
{
    public static class Messages
    {
        //PRODUCT
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductDeleted = "Ürün Silindi";
        public static string ProductUpdated = "Ürün Güncellendi";
        public static string ProductShown = "Ürün Gösterildi";
        public static string ProductIsNotAvailable = "Bu ürün Kayıtlı değil. Lütfen önce ürünü kayıt ediniz";
        public static string ProductIsAvailable = "Bu ürün zaten sisteme kayıtlı tekrar eklenemez";
        public static string AddToCart = "Ürün sepete eklendi";
        public static string NotEnoughProduct = "Markette yeterli ürün yok!";
        public static string DeleteToCart = "ürün sepetten çıkartıldı";
        //LOGIN-REGISTER
        public static string UserRegistered = "Kullanıcı Başarıyla Kaydedildi";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token Oluşturuldu";
        //YETKİLENDİRME
        public static string AuthorizationDenied = "Yetkiniz Yok";
        //ŞİKAYET-COMPLAINT
        public static string ComplaintAdded = "Şikayetiniz için teşekkür ederiz. Yönetici tarafından bilgilendirileceksiniz.";
        public static string ComplaintDeleted = "şikayetiniz silindi";
        public static string ComplaintUpdateisChecked = "Şikayet Durumu kontrol edildi.";
        public static string ProductIsNotAvailableForComplaint = "Bu ürün, bu markette satılmamaktadır.";

        //MÜDÜR-MARKET ELEMANI
        public static string AssignWorkManSuccessfully = "Market Elemanı Ataması Başarılı";

        //SEPET
        public static string CartAdded = "Sepete Eklendi";
    }
}
