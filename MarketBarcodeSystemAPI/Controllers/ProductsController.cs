using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketBarcodeSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //YAPILDI------------------------------------
        //ischecked alanı, şikayet edliriken gönderdildiğinde her zman false 
        //gidecek müdür kontrol ettirip düzeltince ischecked true olacak. 
        //false olduğunda user şikayetine kontrol edildi diye görecek.
        //fluentvalidation ile bir liste yap bu listeye argo kelimeleri ekle
        //şikayet edilirken bu argo kelimeler kullanılmış ise bir mesaj döndür
        //ve argo kelime kullanma diye düzelt.

        //müdür şikayeti düzeltince kusura bakmayın bunun için 
        //gibisindien bir özür mesajı göndersin.


        //yapıldı----------------------------
        //Account tablosuna el ile ekleme yapılabilir. Bu tabloda Userid
        //alanı müdürün userid si olacak buraya sadece ben kayıt ekler veya 
        //günceller veya silerim. buranın herhangi bir fonksiyonu olmayacak.
        //dışarıdan erişilip işlem yapılabilir olmayacak. Bu şikayetleri 
        //hangi kuruma gidecek onu ayarlamak için yaptım. bu kısıma başka
        //implementasyonlarda daha sonra eklenebilir. Burdaki AccountId bile 
        //el ile verilecek.


        //market elemanlarını sadece müdür atayabilir. user tablosuna bir alan ekle. müdür bu alanı 
        //true gönderirse bu bir market elemanı olacak. yada bir sayfa olacak. müdür
        //market elemanı ekle tuşuna bastığında o sayfa açılacak. ekle tuşuna basınca
        //kayıt et ve bu alanı true yap. yada isWorkMan alanı her zaman fase olacak
        //müdür kayıtlı olan userleri görecek bu userlerde arama yeri olacak elemanı
        //arayacak bulacak ve market elemanı olarak ayarla deyince bu alan true olacak.

        //YAPILDI------------------------------------
        //business da update methodu kullanarak isWorkMan alanını güncelleyeceksin
        //bu müdür tarafından market elemanı atayabilmek için.

        //YAPILDI----------------------------------
        //Product tablosuna accountid ekle. bu ürün hangi markette bulunuyor bilmeliyim.

        //Complaint tablosundaki date alanını frontend ten date.Now neyse onu gönderecek.
        //yani şikayet eklenirken.

        //müdür user listesinden bazı kullanıcıları engelleyebilecek. en son iş bu.

        //DTO dan gönderdiğin müdürün görmesi gereken user listesi var. Burada passwordhash
        //ve PasswortSalt e gönderirken frontend den bir çözüm bulmalısın.

        //Ürünlerin dışardan markete gelirken bir implementasyonu yapılabilir. sonraki aşama

        //sepeti onayla tuşu olacak buna tıklandığında karekod oluşacak
        //kasiyer bu karekodu bişeyle okuttuğu anda websitesinde bilgiler görünecek.
        //sonra kasiyer manuel olarak poşeti kont. edecek. herşey normalse onayı verecek.

        //kullanıcı ürün alım satım mı yapacak yoksa başka bir şey mi yapacak ona göre sayfalar değişebilir.

        //şikayet edilen ürünü müdür görünce düzeltmek için market elemanına bildirim gibi bişey gönderecek
        //market elemanı sorunu giderdiğinde sorun giderildi tuşuna basacak ve hem müdüre hemde şikayet eden
        //kişiye gerekli bildirimler gidecek. aynı zmanda müdür elemaı telno dan arayabilip değiştirtebilecek.

        //YAPILDI------------------------------------
        //şikayet edilirken ürünün okutulduğu barcodeid veritabanında varmı onu kont. et bu product tablosuna 
        //yeni alan eklenmeli accountid. where şartında hem barcodeid hemde accountid si eşleşenleri şi,kayet 
        //olarak kaydet.

        //getcomplaint şikeyet ettiği markete
        //göre ayrı ayrı listeleme yap(accountid ye göre frontend de bu ayrıştırılabilir).

        //argo kelime kullanılırsa, hatayı frontende gönder.

        //User hangi marketin ürünü şikayet edecek. Bunun için bir çözüm bulmalısın... aslında çözümü yapmışsın
        //çünkü artık product tablonda accountid var. bir DTO yaz. şikayet etmek istediği ürünü kullanıcı barkoda
        //okuttuktan sonra şikayet et tuşuna bastıktan sonra bu dto ile accountid ve account nameyi çek ekranda
        //şikayet etme sayfasında bunları da göster.
        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(long id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addtocart")]
        public IActionResult AddToCart(Product product, int NumberOfProducts)
        {
            var result = _productService.AddToCart(product, NumberOfProducts);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletetocart")]
        public IActionResult DeleteToCart(Product product, int NumberOfProducts)
        {
            var result = _productService.DeleteToCart(product, NumberOfProducts);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
