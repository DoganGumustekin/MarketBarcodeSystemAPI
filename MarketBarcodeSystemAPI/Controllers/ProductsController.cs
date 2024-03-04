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

        //1YAPILDI------------------------------------??
        //ischecked alanı, şikayet edliriken gönderdildiğinde her zman false 
        //gidecek müdür kontrol ettirip düzeltince ischecked true olacak. 
        //false olduğunda user şikayetine kontrol edildi diye görecek.
        //fluentvalidation ile bir liste yap bu listeye argo kelimeleri ekle
        //şikayet edilirken bu argo kelimeler kullanılmış ise bir mesaj döndür
        //ve argo kelime kullanma diye düzelt.

        //2müdür şikayeti düzeltince kusura bakmayın bunun için 
        //gibisindien bir özür mesajı göndersin.


        //3yapıldı----------------------------
        //Account tablosuna el ile ekleme yapılabilir. Bu tabloda Userid
        //alanı müdürün userid si olacak buraya sadece ben kayıt ekler veya 
        //günceller veya silerim. buranın herhangi bir fonksiyonu olmayacak.
        //dışarıdan erişilip işlem yapılabilir olmayacak. Bu şikayetleri 
        //hangi kuruma gidecek onu ayarlamak için yaptım. bu kısıma başka
        //implementasyonlarda daha sonra eklenebilir. Burdaki AccountId bile 
        //el ile verilecek.

        //4account tablosu ile şikayet işlemi yapılırken kullanıcı barkodu okutacak sonrasında 
        //ürün bilgileri gelecek orada bir şikayet et butonu olacak ve ona basınca buradaki 
        //accountname ler listelenecek. burdan kullanıcı arama yapabilecek. arayıp satın aldığı
        //marketi bulacak ve üzerine tıkladıktan sonra şikayet detayları sayfası açılacak.


        //5market elemanlarını sadece müdür atayabilir. user tablosuna bir alan ekle. müdür bu alanı 
        //true gönderirse bu bir market elemanı olacak. yada bir sayfa olacak. müdür
        //market elemanı ekle tuşuna bastığında o sayfa açılacak. ekle tuşuna basınca
        //kayıt et ve bu alanı true yap. yada isWorkMan alanı her zaman fase olacak
        //müdür kayıtlı olan userleri görecek bu userlerde arama yeri olacak elemanı
        //arayacak bulacak ve market elemanı olarak ayarla deyince bu alan true olacak.

        //6YAPILDI------------------------------------
        //business da update methodu kullanarak isWorkMan alanını güncelleyeceksin
        //bu müdür tarafından market elemanı atayabilmek için.

        //7YAPILDI----------------------------------
        //Product tablosuna accountid ekle. bu ürün hangi markette bulunuyor bilmeliyim.

        //8Burası complaint modeldeki apideki kısımda yapıldı-------------------------------
        //Complaint tablosundaki date alanını frontend ten date.Now neyse onu gönderecek.
        //yani şikayet eklenirken.

        //9müdür user listesinden bazı kullanıcıları engelleyebilecek. en son iş bu.

        //10DTO dan gönderdiğin müdürün görmesi gereken user listesi var. Burada passwordhash
        //ve PasswortSalt e gönderirken frontend den bir çözüm bulmalısın.

        //11Ürünlerin dışardan markete gelirken bir implementasyonu yapılabilir. sonraki aşama

        //12sepeti onayla tuşu olacak buna tıklandığında karekod oluşacak
        //kasiyer bu karekodu bişeyle okuttuğu anda websitesinde bilgiler görünecek.
        //sonra kasiyer manuel olarak poşeti kont. edecek. herşey normalse onayı verecek.

        //13şikayet edilen ürünü müdür görünce düzeltmek için market elemanına bildirim gibi bişey gönderecek
        //market elemanı sorunu giderdiğinde sorun giderildi tuşuna basacak ve hem müdüre hemde şikayet eden
        //kişiye gerekli bildirimler gidecek. aynı zmanda müdür elemaı telno dan arayabilip değiştirtebilecek.

        //14YAPILDI------------------------------------
        //şikayet edilirken ürünün okutulduğu barcodeid veritabanında varmı onu kont. et bu product tablosuna 
        //yeni alan eklenmeli accountid. where şartında hem barcodeid hemde accountid si eşleşenleri şi,kayet 
        //olarak kaydet. BUNU 4 NUMARALI İLE DEĞİŞTİREBİLİRSİN 4 DAHA İYİ

        //15getcomplaint şikeyet ettiği markete
        //göre ayrı ayrı listeleme yap(accountid ye göre frontend de bu ayrıştırılabilir).

        //16argo kelime kullanılırsa, hatayı frontende gönder.

        //17User hangi marketin ürünü şikayet edecek. Bunun için bir çözüm bulmalısın... aslında çözümü yapmışsın
        //çünkü artık product tablonda accountid var. bir DTO yaz. şikayet etmek istediği ürünü kullanıcı barkoda
        //okuttuktan sonra şikayet et tuşuna bastıktan sonra bu dto ile accountid ve account nameyi çek ekranda
        //şikayet etme sayfasında bunları da göster.ŞÜPHELİ

        //18market kasiyeri karekodu okuttuktan sonra mobil ekranında beklemeye geçilecek. orada satışın website üzerinden
        //yapılması bekleniyor diye bir mesaj görünecek ve uygulama kullanılabilirliği kapatılacak. ürün satışı onaylanıp
        //veya onaylanmadığında mobil uygulama burayı dinleyecek ve ona göre tekrar barkod okutma sayfasına otomatik
        //döndürecek. bu kısım için frontend mobil de ayrı bir karekod okutma sayfası olacak. eğer orada karekodu okutursak
        //o websiteye gidecek.

        //18.2Bu kısım için müşteri sepeti onayla tuşuna basınca karekod oluşacak. bu karekodu kasiyer telden okutacak.
        //okuttuktan sonra backgroundservice ile, kasiyerin okuttuğu qr kod sürekli dinlenecek. bunun için bir fonksiyon
        //yazılacak. bir veri gelmesi halinde otomatikmen veri websitesine aktarılacak.(Burada oluşan sorun, farklı farklı
        //marketler birbirini etkilememeli. burada rabbitmq ile bir çözüm bulunabilir. account tablosuna yeni alan eklenir.
        //(AccountOrderID) buna account ismine paralel bir isim verilir. bu isim rabbitmq daki route isminin yanına eklenir.
        //(AccountOrderID + queueName(route name)) sonra burdan bir kuyruk oluşur(direct exchange)
        //buradan routekey account bazlı özelleştirildiği için her account kendi tarafındaki durumu dinler. Ama resposne gönderilirken de
        //bu durumu ayrıştırabilen başka bir rabbirmmq kuyruğu oluşturulur. buda yine o account isminde olacak ve dönen responsede
        //bir success mesajı döndürülür. yada tek kuyruklada olabilir. hangisi mantıklıysa o yapılacak. rabbitmq kuyruğunu
        //dinleyen backgroundservice kısmında queuename ile account tablosundaki AccountOrderID eşleşeceği için ef ile
        //bir sorgulama yapışlır ve sadece ilgili accounta response gönderilir.)

        //19veritabanındaki id ler neden 1000 artıyor bunu çöz kesinlikle localindeki sorunu.

        //product tablosuna productRate diye bir alan ekle. ürüne puan verilebilsin.(ekstra)

        //Siparişler tablosu olacak. burada hangis user hangi ürünü satın almışsa onu tutacaksın. User login olduktan sonra 
        //kendi ana sayfasında daha önce aldığı ürünleri görebilir gibi bişey yapabilsin.

        //website den onaylarken eleman qr kodu okutacak sonrasında websitede userin adı da yazılabilir ekrana siparişş web
        //siteden onaylandığında, sipariş tablosuna kayıt atılabilir.
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
