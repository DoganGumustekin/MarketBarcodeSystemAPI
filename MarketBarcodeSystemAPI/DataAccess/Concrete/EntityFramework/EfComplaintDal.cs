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
        //müdürün göreceği şikayet listesi.
        public List<ComplaintForManagerModel> GetComplaintsForManager(int AccountKey)
        {
            using (var context = new MarketManagementContext())
            {
                var result = (from complaint in context.Complaints
                              join product in context.Products on complaint.BarcodeId equals product.BarcodeId into productJoin
                              from product in productJoin.DefaultIfEmpty()
                              join user in context.User on complaint.UserId equals user.UserId into userJoin
                              from user in userJoin.DefaultIfEmpty()
                              where complaint.AccountKey == AccountKey
                              orderby complaint.ComplaintDate descending
                              select new ComplaintForManagerModel
                              {
                                  ComplaintId = complaint.ComplaintId,
                                  AccountKey = complaint.AccountKey,
                                  BarcodeId = product.BarcodeId,
                                  ProductName = product.ProductName,
                                  UserId = user.UserId,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  ComplaintDescription = complaint.ComplaintDescription,
                                  isChecked = complaint.isChecked,
                                  ComplaintDate = complaint.ComplaintDate,
                                  ComplaintCheckDate = complaint.ComplaintCheckDate
                              }).ToList();

                return result;
            }
        }

        public List<ComplaintForUserModel> GetComplaintsForUser(int userId)
        {
            using (var context = new MarketManagementContext())
            {
                var result = (from complaint in context.Complaints
                              where complaint.UserId == userId
                              join account in context.Accounts on complaint.AccountKey equals account.AccountKey into accountJoin
                              from account in accountJoin.DefaultIfEmpty()
                              join product in context.Products on complaint.BarcodeId equals product.BarcodeId into productJoin
                              from product in productJoin.DefaultIfEmpty()
                              group new { complaint, product, account } by complaint.ComplaintId into grouped
                              orderby grouped.Select(g => g.complaint.ComplaintDate).FirstOrDefault() descending
                              select new ComplaintForUserModel
                              {
                                  ComplaintId = grouped.Key,
                                  AccountKey = grouped.Select(g => g.account.AccountKey).FirstOrDefault(),
                                  BarcodeId = grouped.Select(g => g.product.BarcodeId).FirstOrDefault(),
                                  UserId = grouped.Select(g => g.complaint.UserId).FirstOrDefault(),
                                  ProductName = grouped.Select(g => g.product.ProductName).FirstOrDefault(),
                                  AccountName = grouped.Select(g => g.account.AccountName).FirstOrDefault(),
                                  ComplaintDescription = grouped.Select(g => g.complaint.ComplaintDescription).FirstOrDefault(),
                                  isChecked = grouped.Select(g => g.complaint.isChecked).FirstOrDefault(),
                                  ComplaintCheckDate = grouped.Select(g => g.complaint.ComplaintCheckDate).FirstOrDefault()
                              }).ToList();

                return result;
            }
        }


    }
}
