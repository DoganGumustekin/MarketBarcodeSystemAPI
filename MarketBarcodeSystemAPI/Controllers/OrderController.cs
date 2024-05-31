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
        public IActionResult Add(List<Order> orders, int managerUserId)
        {
            var result = _orderService.AddOrders(orders,managerUserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("OrderCanceled")]
        public IActionResult OrderCanceled(int userId)
        {
            var result = _orderService.OrderCanceled(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
