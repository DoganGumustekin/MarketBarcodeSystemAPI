using Autofac.Extensions.DependencyInjection;
using Autofac;
using MarketBarcodeSystemAPI.Business.DependencyResolvers.Autofac;
using MarketBarcodeSystemAPI.Core.Utilities.Security.JWT;
using Core.Utilities.IoC;
using Autofac.Core;
using Core.Extensions;
using MarketBarcodeSystemAPI.Core.DependencyResolvers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Encryption;
using MarketBarcodeSystemAPI.Business.Concrete;

var builder = WebApplication.CreateBuilder(args);



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidIssuer = tokenOptions.Issuer,
                       ValidAudience = tokenOptions.Audience,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                   };
               });

builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
});
//ServiceTool.Create(builder.Services);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();



app.Run();
