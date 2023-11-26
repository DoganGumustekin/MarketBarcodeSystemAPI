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
    }
}
