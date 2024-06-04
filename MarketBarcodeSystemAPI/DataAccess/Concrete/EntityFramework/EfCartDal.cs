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
        public List<CartForOrderModel> GetCartProducts(int userId, int accountKey)
        {
            using (var context = new MarketManagementContext())
            {
                var result = (from cart in context.Cart
                              join product in context.Products on cart.BarcodeId equals product.BarcodeId into productJoin
                              from product in productJoin.DefaultIfEmpty()
                              join user in context.User on cart.UserId equals user.UserId into userJoin
                              from user in userJoin.DefaultIfEmpty()
                              where cart.UserId == userId && cart.AccountKey == accountKey
                              group new { cart, product } by new
                              {
                                  cart.CartId,
                                  cart.UserId,
                                  cart.BarcodeId,
                                  cart.AccountKey,
                                  cart.NumberOfProduct
                              } into grouped
                              select new CartForOrderModel
                              {
                                  CartId = grouped.Key.CartId,
                                  UserId = grouped.Key.UserId,
                                  BarcodeId = grouped.Key.BarcodeId,
                                  AccountKey = grouped.Key.AccountKey,
                                  ProductName = grouped.Select(g => g.product != null ? g.product.ProductName : null).FirstOrDefault(),
                                  ProductPrice = grouped.Select(g => g.product != null ? g.product.ProductPrice : 0).FirstOrDefault(),
                                  NumberOfProduct = grouped.Key.NumberOfProduct
                              }).ToList();

                return result;
            }
        }


    }
}
