using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.DataAccess.EntityFramework;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework
{
    public class EfComplaintDal : EfEntityRepositoryBase<Complaint, MarketManagementContext>, IComplaintDal
    {
        public List<ComplaintForManagerModel> GetComplaintsForManager(Account account)
        {
            using (var context = new MarketManagementContext())
            {
                var result = (from complaint in context.Complaints
                              join product in context.Products on complaint.BarcodeId equals product.BarcodeId into productJoin
                              from product in productJoin.DefaultIfEmpty()
                              join acc in context.Accounts on complaint.AccountId equals acc.AccountId into accountJoin
                              from acc in accountJoin.DefaultIfEmpty()
                              join user in context.User on complaint.UserId equals user.UserId into userJoin
                              from user in userJoin.DefaultIfEmpty()
                              where acc.AccountId == account.AccountId
                              orderby complaint.ComplaintDate descending
                              select new ComplaintForManagerModel
                              {
                                  ComplaintId = complaint.ComplaintId,
                                  AccountId = acc.AccountId,
                                  BarcodeId = product.BarcodeId,
                                  ProductName = product.ProductName,
                                  UserId = user.UserId,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  ComplaintDescription = complaint.ComplaintDescription,
                                  isChecked = complaint.isChecked,
                                  ComplaintDate = complaint.ComplaintDate
                              });

                return result.ToList();
            }
        }

    }
}
