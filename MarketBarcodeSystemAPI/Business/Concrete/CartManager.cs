using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class CartManager:ICartService
    {
        ICartDal _CartDal;
        public CartManager(ICartDal CartDal)
        {
            _CartDal = CartDal;
        }

        public IDataResult<List<CartForOrderModel>> GetCartProducts(int userId)
        {
            return new SuccessDataResult<List<CartForOrderModel>>(_CartDal.GetCartProducts(userId));
        }
    }
}
