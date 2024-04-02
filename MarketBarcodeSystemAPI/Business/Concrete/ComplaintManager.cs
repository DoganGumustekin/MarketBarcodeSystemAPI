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
        IProductDal _productDal;

        public ComplaintManager(IComplaintDal complaintDal, IProductDal productDal)
        {
            _complaintDal = complaintDal;
            _productDal = productDal;

        }

        [ValidationAspect(typeof(ComplaintValidator))]
        public IResult AddComplaint(Complaint complaint)
        {
            IResult result = BusinessRules.Run(IsThisProductAvailable(complaint.BarcodeId,complaint.AccountId));
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
        public IDataResult<List<ComplaintForManagerModel>> GetComplaintsForManager(int accountId)
        {
            return new SuccessDataResult<List<ComplaintForManagerModel>>(_complaintDal.GetComplaintsForManager(accountId));
        }

        //Müdürün Complaintin ischeckedini true yapması.
        public IResult ComplaintChecked(Complaint complaint)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _complaintDal.Update(complaint);
            return new SuccessResult(Messages.ComplaintUpdateisChecked);
        }







        private IResult IsThisProductAvailable(long barcodeId, int accountId)
        {
            var result = _productDal.GetAll(p => p.BarcodeId == barcodeId && p.AccountId == accountId).Any();
            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.ProductIsNotAvailable);
        }
    }
}
