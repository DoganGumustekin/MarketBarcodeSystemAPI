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
        public List<ComplaintForManagerModel> GetComplaintsForManager(int accountId)
{
    using (var context = new MarketManagementContext())
    {
        var result = (from complaint in context.Complaints
                      join product in context.Products on complaint.BarcodeId equals product.BarcodeId into productJoin
                      from product in productJoin.DefaultIfEmpty()
                      join user in context.User on complaint.UserId equals user.UserId into userJoin
                      from user in userJoin.DefaultIfEmpty()
                      where complaint.AccountId == accountId
                      orderby complaint.ComplaintDate descending
                      select new ComplaintForManagerModel
                      {
                          ComplaintId = complaint.ComplaintId,
                          AccountId = complaint.AccountId,
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



        //Şikayet ekleme joini
        //public ComplaintParamModel GetComplaintForAdd(Complaint complaint)
        //{
        //    using (var context = new MarketManagementContext())
        //    {
        //        var result = (from c in context.Complaints
        //                      join p in context.Products on c.BarcodeId equals p.BarcodeId into productJoin
        //                      from product in productJoin.DefaultIfEmpty()
        //                      join a in context.Accounts on c.AccountId equals a.AccountId into accountJoin
        //                      from account in accountJoin.DefaultIfEmpty()
                              
        //                      where complaint.ComplaintId == c.ComplaintId && 
        //                      complaint.AccountId == account.AccountId && 
        //                      complaint.BarcodeId == product.BarcodeId
                              
        //                      select new ComplaintParamModel
        //                      {
        //                          ComplaintId = c.ComplaintId,
        //                          AccountId = c.AccountId,
        //                          AccountName = account.AccountName,
        //                          BarcodeId = product.BarcodeId,
        //                          UserId = c.UserId,
        //                          ComplaintDescription = c.ComplaintDescription,
        //                          isChecked = c.isChecked,
        //                          ComplaintDate = c.ComplaintDate
        //                      }).FirstOrDefault();

        //        return result;
        //    }
        //}

    }
}
