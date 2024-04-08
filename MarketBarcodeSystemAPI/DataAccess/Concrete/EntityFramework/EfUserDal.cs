using Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Core.DataAccess.EntityFramework;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, MarketManagementContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new MarketManagementContext())
            {
                var result = from operationClaim in context.OperationClaims
                                join userOperationClaim in context.UserOperationClaims //operationclaimlerle useroperationclaimlere join atar.
                                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.UserId
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public List<UserListModel> GetUsers()
        {
            using (var context = new MarketManagementContext())
            {
                    var result = context.User
                .Select(u => new UserListModel
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    isWorkMan = u.isWorkMan,
                    Status = u.Status
                })
                .ToList();

                return result;
            }

        }

        public List<WorkmanListModel> GetWorkmans(int AccountKey)
        {
            using (var dbContext = new MarketManagementContext())
            {
                var result = (from user in dbContext.User
                            join userOpClaim in dbContext.UserOperationClaims on user.UserId equals userOpClaim.UserId into userOpClaimGroup
                            from userOpClaim in userOpClaimGroup.DefaultIfEmpty()
                            join opClaim in dbContext.OperationClaims on userOpClaim.OperationClaimId equals opClaim.Id into opClaimGroup
                            from opClaim in opClaimGroup.DefaultIfEmpty()
                            join account in dbContext.Accounts on user.UserId equals account.UserId into accountGroup
                            from account in accountGroup.DefaultIfEmpty()
                            where account != null && account.AccountKey == AccountKey
                              select new WorkmanListModel
                            {
                                UserId = user.UserId,
                                AccountId = account.AccountId,
                                AccountName = account.AccountName,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Email = user.Email,
                                Name = opClaim != null ? opClaim.Name : null
                            }).ToList();
                return result;
            }
        }
    }
}
