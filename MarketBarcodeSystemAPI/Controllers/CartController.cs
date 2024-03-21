using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketBarcodeSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet("GetCartProducts")]
        public IActionResult GetCartProducts(int userId)
        {
            var result = _cartService.GetCartProducts(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
