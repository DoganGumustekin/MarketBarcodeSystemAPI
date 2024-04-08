using MarketBarcodeSystemAPI.Core.DataAccess.EntityFramework;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework
{
    public class EfCartDal : EfEntityRepositoryBase<Cart, MarketManagementContext>, ICartDal
    {
        public List<CartForOrderModel> GetCartProducts(int userId)
        {
            using (var context = new MarketManagementContext())
            {
                var result = (from cart in context.Cart
                              join product in context.Products on cart.BarcodeId equals product.BarcodeId into productJoin
                              from product in productJoin.DefaultIfEmpty()
                              join user in context.User on cart.UserId equals user.UserId into userJoin
                              from user in userJoin.DefaultIfEmpty()
                              where cart.UserId == userId
                              select new CartForOrderModel
                              {
                                  CartId = cart.CartId,
                                  UserId = cart.UserId,
                                  BarcodeId = cart.BarcodeId,
                                  AccountKey = product != null ? product.AccountKey : 0,
                                  ProductName = product != null ? product.ProductName : null,
                                  ProductPrice = product != null ? product.ProductPrice : 0,
                                  NumberOfProduct = cart.NumberOfProduct
                              }).ToList();

                return result;
            }
        }

    }
}
