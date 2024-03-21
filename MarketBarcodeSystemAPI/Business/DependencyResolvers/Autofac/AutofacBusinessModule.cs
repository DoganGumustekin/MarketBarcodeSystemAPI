using Autofac;
using Autofac.Extras.DynamicProxy;
using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Concrete;
using MarketBarcodeSystemAPI.Core.Utilities.Interceptors;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework;
using System.Reflection;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;
using DataAccess.Concrete.EntityFramework;
using MarketBarcodeSystemAPI.Core.Utilities.Security.JWT;

namespace MarketBarcodeSystemAPI.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<ComplaintManager>().As<IComplaintService>().SingleInstance();
            builder.RegisterType<EfComplaintDal>().As<IComplaintDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<CartManager>().As<ICartService>().SingleInstance();
            builder.RegisterType<EfCartDal>().As<ICartDal>().SingleInstance();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); //çalışan uygulama içerisinde 

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //inplemente edilmiş interfaceleri bul 
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() //onlar için aspectinterceptorselector u çağır. 
                }).SingleInstance();                            //autofac bizim bütün sınıflarımız için önce bunu
                                                                //çalıştırır bu adamın aspect i varmı
        }
    }
}
