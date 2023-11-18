using Core.Utilities.IoC;
using System.Diagnostics;

namespace MarketBarcodeSystemAPI.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//.addmemorycache = .net kendisi otomatik injection yapıyor.
            //bunu yapmalıyız çünkü _memoryCache nin karşılığı yok. 
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //her yapılan istekle oluşan context
            //serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>(); //Core.Aspects.Autofac.Performance
        }
    }
}
