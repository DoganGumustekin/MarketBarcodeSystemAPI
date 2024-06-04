using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
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


        [HttpGet("GetCartProductsList")]
        public IActionResult GetCartProducts(int userId, int accountKey)
        {
            var result = _cartService.GetCartProductsForCustomer(userId, accountKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("DeleteProductInCart")]
        public IActionResult DeleteProductInCart(Cart cart)
        {
            var result = _cartService.DeleteProductInCart(cart);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
