using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Abstract
{
    public interface IOrderService
    {
        IResult AddOrders(List<Order> orders, int managerUserId);
        IResult OrderCanceled(int userId);
    }
}
