using MarketBarcodeSystemAPI.Core.DataAccess;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.DataAccess.Abstract
{
    public interface IAccountDal : IEntityRepository<Account>
    {
    }
}
