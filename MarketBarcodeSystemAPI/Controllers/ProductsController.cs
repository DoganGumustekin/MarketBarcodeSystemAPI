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

        //[HttpPost("yazıyaz")]
        //public IActionResult yazdir(string text)
        //{
        //    var dosyaYolu = "C:\\x\\Yeni Metin Belgesi.txt";
        //    using (StreamWriter Yaz = new StreamWriter(dosyaYolu))
        //    {
        //        Yaz.Write(text);
        //    };
            

        //    return Ok();
        //}

        //Kullanıcı ürünü barkottan okutacak(getbyid) ürün gelecek ve ürünün altında 
        //güncelle veya sil olacak bunlara bastığında delete veya update çalışacak!!!!!!
        //yani önce getbyid sonra Delete update. Bu duruma göre de delete veya update nin 
        //yapısının değişip değişmeyeceğini teyit et. Postmu olacak get mi yada ben zaten 
        //getbyid ile getirmişim onu delete ve update de kullanırken delete tuşuna delete 
        //controllerini, update tuşuna ise update controllerini bağlamalıyım.


        //esp32 ile bir cihaz tasarlayıp, kasadaki sisteme bağlayıp buradan wifi ile kendi mobil uygulamama stok verilerini gönderebilirim.
        //Kendi mobil uygulamamda backgroundService ile bu verileri sürekli yakalayıp (esp32 den gelen vei bir değişkene aktarılır.
        //Daha sonra if gelenVeri == null ise işlem yapma Else bunu veritabanından düşür.) stoktan düşürebilirim.

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
