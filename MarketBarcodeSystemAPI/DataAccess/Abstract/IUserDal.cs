using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.DataAccess;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;

namespace MarketBarcodeSystemAPI.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
