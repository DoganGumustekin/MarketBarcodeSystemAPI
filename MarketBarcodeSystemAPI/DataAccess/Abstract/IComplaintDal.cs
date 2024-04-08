using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.DataAccess;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.DataAccess.Abstract
{
    public interface IComplaintDal:IEntityRepository<Complaint>
    {
        List<ComplaintForManagerModel> GetComplaintsForManager(int AccountKey);
        List<ComplaintForUserModel> GetComplaintsForUser(int userId);
    }
}
