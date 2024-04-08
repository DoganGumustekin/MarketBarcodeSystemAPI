using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Core.Aspects.Autofac.Validation;
using MarketBarcodeSystemAPI.Core.Entities;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework;
using MarketBarcodeSystemAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public IResult GetAccountIdForAdmin(int userId)
        {
            return new SuccessDataResult<Account>(_accountDal.Get(p => p.UserId == userId));
        }

        public IDataResult<List<WorkmanListModel>> WorkManList(int AccountKey)
        {
            return new SuccessDataResult<List<WorkmanListModel>>(_userDal.GetWorkmans(AccountKey));
        }

        public IResult DeleteWorkMan(int userId)
        {
            using (MarketManagementContext context = new MarketManagementContext())
            {
                Account? accountToDelete = context.Accounts.FirstOrDefault(a => a.UserId == userId);
                UserOperationClaim? userOperationClaimToDelete = context.UserOperationClaims.FirstOrDefault(uoc => uoc.UserId == userId);

                if (accountToDelete != null && userOperationClaimToDelete != null)
                {
                    context.Accounts.Remove(accountToDelete);
                    context.UserOperationClaims.Remove(userOperationClaimToDelete);
                    context.SaveChanges();
                    return new SuccessResult(Messages.DeletedWorkMan);
                }
                else
                {
                    return new ErrorResult(Messages.WorkmanDeletedError);
                }
            }
        }

        public IResult WorkManAdd(string eMail, Account account)
        {
            using (MarketManagementContext context = new MarketManagementContext())
            {
                var user = context.User.FirstOrDefault(u => u.Email == eMail);
                if (user != null)
                {
                    UserOperationClaim userOperationClaim = new UserOperationClaim();
                    userOperationClaim.UserId = user.UserId;
                    userOperationClaim.OperationClaimId = 2;
                    context.UserOperationClaims.Add(userOperationClaim);
                    _accountDal.Add(account);
                    context.SaveChanges();
                    return new SuccessResult(Messages.AssignWorkManSuccessfully);
                }
                return new ErrorResult(Messages.WrongEmail);
            }
        }

        public IDataResult<User> GetUserWithUserEmail(string eMail)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == eMail));
        }
    }
}
