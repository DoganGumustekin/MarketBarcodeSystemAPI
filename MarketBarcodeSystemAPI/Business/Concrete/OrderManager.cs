using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using System.Collections.Generic;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        ICartDal _cartDal;
        IProductDal _productDal;
        IAccountDal _accountDal;
        public OrderManager(IOrderDal orderDal, ICartDal cartDal, IProductDal productDal, IAccountDal accountDal)
        {
            _orderDal = orderDal;
            _cartDal = cartDal;
            _productDal = productDal;
            _accountDal = accountDal;
        }
        public IResult AddOrders(List<Order> orders, int managerUserId)
        {
            IResult result = BusinessRules.Run(DoesItMatchForAccountKey(orders, managerUserId));
            if (result != null)
            {
                return result;
            }
            _orderDal.AddList(orders);
            return new SuccessResult(Messages.OrdersAdded);
        }

        public IResult OrderCanceled(int userId)
        {
            List<Cart> carts = new List<Cart>();
            carts = _cartDal.GetAll(c => c.UserId == userId);
            foreach (var cart in carts)
            {
                var productUpdateForStockQuantity = _productDal.Get(p => p.BarcodeId == cart.BarcodeId);
                productUpdateForStockQuantity.StockQuantity += cart.NumberOfProduct;
                _productDal.Update(productUpdateForStockQuantity);
            }
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _cartDal.DeleteList(carts);
            return new SuccessResult(Messages.OrderCanceled);
        }

        private IResult DoesItMatchForAccountKey(List<Order> orders, int managerUserId)
        {
            var getAccountKeyForOrder = _accountDal.Get(a => a.UserId == managerUserId);
            if (orders.First().AccountKey == getAccountKeyForOrder.AccountKey)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.NotMatchAccountKey);
        }

        //private IResult productUpdateForDeletedCart(List<Cart> carts)
        //{
        //    foreach (var cart in carts)
        //    {


        //    }
        //}
    }
}
