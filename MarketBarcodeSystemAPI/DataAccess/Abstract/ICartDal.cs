using MarketBarcodeSystemAPI.Core.DataAccess;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.DataAccess.Abstract
{
    public interface ICartDal : IEntityRepository<Cart>
    {
        //List<CartForOrderModel> GetCartProducts(int userId);
        List<CartForOrderModel> GetCartProducts(int userId, int accountKey);
    }
}
