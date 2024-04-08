using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        IDataResult<List<UserListModel>> GetUserList();
        IResult WorkManAdd(string eMail, Account account);
        IResult GetAccountIdForAdmin(int userId);
        IDataResult<List<WorkmanListModel>> WorkManList(int AccountKey);
        IResult DeleteWorkMan(int userId);
        IDataResult<User> GetUserWithUserEmail(string eMail);
    }
}
