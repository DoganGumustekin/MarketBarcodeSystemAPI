using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketBarcodeSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("OrderAdd")]
        public IActionResult Add(List<Order> orders)
        {
            var result = _orderService.AddOrder(orders);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
