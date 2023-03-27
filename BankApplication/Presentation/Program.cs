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
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Presentation;
using Services.Services;
using Services.Services.Class;
using Serilog;
using Microsoft.Owin.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Serilog.Formatting.Compact;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers ();
        builder.Services.AddMapster();
        builder.Services.AddMvc().AddNewtonsoftJson();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddApplicationInsightsTelemetry();
        
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
        builder.Services.AddLogging(x =>
        {
            x.ClearProviders();
            x.AddSerilog(dispose: true);
        });
        builder.Services.AddMvc().AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        builder.Services.AddDbContext<BankDatabaseContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("mysqlconnection")
            
                );
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
        });
        builder.Services.AddTransient<IUnitOfWork<BankDatabaseContext>, UnitOfWork<BankDatabaseContext>>();
        //builder.Services.AddTransient<IGenericRepository>, GenericRepository<UnitOfWork>>();
        //builder.Services.AddScoped<IBankRepository, BankRepository>();
        var serviceProvider = builder.Services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();
        builder.Services.AddSingleton(typeof(ILogger), logger);
        builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.MSSqlServer(
                       connectionString: "Server=TL397;Database=BankDatabase;Encrypt=false;Trusted_Connection=True;",
                       tableName: "Logs",
                       appConfiguration: configuration,
                       autoCreateSqlTable: true,

    schemaName: "dbo"
                   )
    .ReadFrom.Configuration(ctx.Configuration));

    //    Log.Logger = new LoggerConfiguration()
    //               .WriteTo.MSSqlServer(
    //                   connectionString: "Server=TL397;Database=BankDatabase;Encrypt=false;Trusted_Connection=True;",
    //                   tableName: "Logs",
    //                   appConfiguration: configuration,
    //                   autoCreateSqlTable: true,
                     
    //schemaName: "dbo"
    //               ).CreateLogger();

        //Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"), Thread.CurrentThread.ManagedThreadId);

        builder.Services.AddScoped<IBankBusiness, BankBusiness>();
        builder.Services.AddScoped<IAccountBusiness,AccountBusiness>();
        //builder.Services.AddSingleton(ILogger,);
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

        builder.Services.AddSingleton<IAuthenticateBusiness>(new AuthenticateBusiness(key, logger));
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


