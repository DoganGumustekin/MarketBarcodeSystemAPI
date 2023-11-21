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

        //şikayet entegrasyonu eklenebilir. kullanıcılar mobil uygulamadan bu ürünü şikayet et 
        //tuşuna basar. herhangi bir sorun var ise o ürünü şikayet edebilir. bu şikayet ise müdürün
        //karşısına çıkar.

        //şikayet tablosu olacak. tablodan joinleyerek user ve ürün bilgilerini
        //çekip müdüre listelet. yeniden eskiye göre (tarih) listelet
        //şikayet et butonuna basıldığında şikayet tablosuna girilen 
        //değerleri kayıt et. müdüre de listelemesini çek. 

        //ischecked alanı, şikayet edliriken gönderdildiğinde her zman true 
        //gidecek müdür kontrol ettirip düzeltince ischecked false olacak. 
        //false olduğunda user şikayetine kontrol edildi diye görecek.
        //fluentvalidation ile bir liste yap bu listeye argo kelimeleri ekle
        //şikayet edilirken bu argo kelimeler kullanılmış ise bir mesaj döndür
        //ve argo kelime kullanma diye düzelt.

        //müdür şikayeti düzeltince kusura bakmayın bunun için 
        //gibisindien bir özür mesajı göndersin.
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
