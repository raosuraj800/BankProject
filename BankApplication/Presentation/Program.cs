using System;
using System.Reflection;
using System.Text;
using System.Web.Http;
using DBLayer;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.Repository;
using DBLayer.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation;
using Services.Services;
using Services.Services.Class;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers ();
        builder.Services.AddMapster();
        builder.Services.AddMvc().AddNewtonsoftJson();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddDbContext<BankDatabaseContext>(options =>
        {
            options.UseSqlServer("Server=TL397;Database=BankDatabase;Trusted_Connection=True;");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
        });
        builder.Services.AddTransient<IUnitOfWork<BankDatabaseContext>, UnitOfWork<BankDatabaseContext>>();
        //builder.Services.AddTransient<IGenericRepository>, GenericRepository<UnitOfWork>>();
        //builder.Services.AddScoped<IBankRepository, BankRepository>();
       
        builder.Services.AddScoped<IBankBusiness, BankBusiness>();
        builder.Services.AddScoped<IAccountBusiness,AccountBusiness>();
        builder.Services.AddMemoryCache();
        TypeAdapterConfig<Bank, BankDataModel>.NewConfig()
                                    .Map(dest => dest, src => src);
        var key = "This is my first Test Key";
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
            };
        });

        builder.Services.AddSingleton<IAuthenticateBusiness>(new AuthenticateBusiness(key));
        var app = builder.Build();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers().RequireAuthorization();

        });
       
    }
}

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        
        return services;
    }
}


