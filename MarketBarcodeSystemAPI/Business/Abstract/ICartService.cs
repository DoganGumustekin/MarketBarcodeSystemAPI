using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.Business.Abstract
{
    public interface ICartService
    {
        IDataResult<List<CartForOrderModel>> GetCartProducts(int userId);
    }
}
