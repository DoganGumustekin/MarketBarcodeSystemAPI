using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IAccountDal _accountDal;

        public UserManager(IUserDal userDal, IAccountDal accountDal)
        {
            _userDal = userDal;
            _accountDal = accountDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user); //claimleri çeker
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        //Müdürün göreceği User Listesi.
        public IDataResult<List<UserListModel>> GetUserList()
        {
            return new SuccessDataResult<List<UserListModel>>(_userDal.GetUsers());
        }

        //Müdürün market elemanı atama işlemi.
        public IResult WorkManUpdate(User user)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.AssignWorkManSuccessfully);
        }

        public IResult GetAccountIdForAdmin(int userId)
        {
            return new SuccessDataResult<Account>(_accountDal.Get(p => p.UserId == userId));
        }
    }
}
