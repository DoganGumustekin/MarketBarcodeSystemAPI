using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public IResult AddOrder(List<Order> orders)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDal.AddList(orders);
            return new SuccessResult(Messages.OrdersAdded);
        }
    }
}
