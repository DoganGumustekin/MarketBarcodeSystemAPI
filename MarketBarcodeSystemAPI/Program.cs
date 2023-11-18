using Autofac.Extensions.DependencyInjection;
using Autofac;
using MarketBarcodeSystemAPI.Business.DependencyResolvers.Autofac;
using MarketBarcodeSystemAPI.Core.Utilities.Security.JWT;
using Core.Utilities.IoC;
using Autofac.Core;
using Core.Extensions;
using MarketBarcodeSystemAPI.Core.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
