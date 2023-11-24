using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Business.ValidationRules.FluentValidation;
using MarketBarcodeSystemAPI.Core.Aspects.Autofac.Validation;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class ComplaintManager : IComplaintService
    {
        IComplaintDal _complaintDal;

        public ComplaintManager(IComplaintDal complaintDal)
        {
            _complaintDal = complaintDal;
        }

        [ValidationAspect(typeof(ComplaintValidator))]
        public IResult AddComplaint(Complaint complaint)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _complaintDal.Add(complaint);
            return new SuccessResult(Messages.ComplaintAdded);
        }

        [ValidationAspect(typeof(ComplaintValidator))]
        public IResult DeleteComplaint(Complaint complaint)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _complaintDal.Delete(complaint);
            return new SuccessResult(Messages.ComplaintDeleted);
        }

        //Buda kullanıcının göreceği kendi şikayet listesi.
        public IDataResult<List<Complaint>> GetComplaints(int userId)
        {
            return new SuccessDataResult<List<Complaint>>(_complaintDal.GetAll(p => p.UserId == userId));
        }

        //Bu müdürün göreceği şikayet listesi.
        public IDataResult<List<ComplaintForManagerModel>> GetComplaintsForManager(Account account)
        {
            return new SuccessDataResult<List<ComplaintForManagerModel>>(_complaintDal.GetComplaintsForManager(account)); //claimleri çeker
        }
    }
}
