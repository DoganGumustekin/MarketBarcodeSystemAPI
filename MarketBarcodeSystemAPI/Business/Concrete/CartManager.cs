using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class CartManager : ICartService
    {
        ICartDal _CartDal;
        IProductDal _productDal;

        public CartManager(ICartDal CartDal, IProductDal productDal)
        {
            _CartDal = CartDal;
            _productDal = productDal;
        }

        public IResult DeleteProductInCart(Cart cart)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _CartDal.Delete(cart);

            var product = _productDal.Get(p => p.BarcodeId == cart.BarcodeId);
            var resultProduct = product.StockQuantity + cart.NumberOfProduct;
            product.StockQuantity = resultProduct;
            _productDal.Update(product);

            return new SuccessResult(Messages.cartDeleted);
        }

        public IDataResult<List<CartForOrderModel>> GetCartProducts(int userId)
        {
            return new SuccessDataResult<List<CartForOrderModel>>(_CartDal.GetCartProducts(userId));
        }
    }
}
