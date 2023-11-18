using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.IoC;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Core.Utilities.Interceptors;

namespace MarketBarcodeSystemAPI.Business.BusinessAspects.Autofac
{
    public class SecuredOperation: MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; 
                                                           
        public SecuredOperation(string roles) //bana rolleri ver 
        {
            _roles = roles.Split(','); 
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); 

        }

        protected override void OnBefore(IInvocation invocation) //methodun önünde çalıştır.
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//o anki kullanıcının claim rollerini bul 
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))//claimlerinin içinde ilgili rol varsa 
                {
                    return; //methodu çalıştırmaya devam et yani claimi varsa onebefore yi bitir hata verme 
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //yoksa yetkin yok hatası ver
        }
        //https://github.com/ismailkaygisiz/ArtChitecture/blob/main/Core/Utilities/Interceptors/MethodInterception.cs
    }
}
