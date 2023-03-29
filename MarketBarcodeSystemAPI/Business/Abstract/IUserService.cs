using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;

namespace MarketBarcodeSystemAPI.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
    }
}
