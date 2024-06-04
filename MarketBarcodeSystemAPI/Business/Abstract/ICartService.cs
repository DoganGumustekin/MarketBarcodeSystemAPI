using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Abstract
{
    public interface ICartService
    {
        IDataResult<List<CartForOrderModel>> GetCartProductsForCustomer(int userId, int accountKey);
        IResult DeleteProductInCart(Cart cart);
    }
}
