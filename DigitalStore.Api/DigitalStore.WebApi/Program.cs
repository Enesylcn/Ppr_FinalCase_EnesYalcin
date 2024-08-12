using Autofac;
using Autofac.Extensions.DependencyInjection;
using DigitalStore.Base.Token;
using DigitalStore.Base;
using DigitalStore.Data.Domain;
using DigitalStore.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using DigitalStore.Business.RegistrationServices;
using Autofac.Core;
using DigitalStore.Data.Context;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using DigitalStore.Business.Validation;
using FluentValidation.AspNetCore;
using System;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using DigitalStore.WebApi.Filters;
using Microsoft.Extensions.Hosting;
using DigitalStore.WebApi.Extensions;

namespace Papara.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

            // Serilog konfigürasyonu
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            Log.Information("Application is starting...");

            var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            builder.Services.AddSingleton(jwtConfig);


            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                #region Password Options
                options.Password.RequiredLength = 6;// Parola en az 6 karakter olmalý
                options.Password.RequireDigit = true;//Parola en az 1 adet sayýsal deðer içermeli
                options.Password.RequireNonAlphanumeric = true;//Parola özel karakter içermeli
                options.Password.RequireUppercase = true;// Parola büyük harf içermeli
                options.Password.RequireLowercase = true;// Parola küçük harf içermeli
                                                         //options.Password.RequiredUniqueChars = // tekrar etmemesi istenilen karakterleri dizi þeklinde verip kullanýlýyor. 
                #endregion
                #region Hesap Kilitleme Ayarlarý
                options.Lockout.MaxFailedAccessAttempts = 3; // Üst üste hatalý giriþ denemesi
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15); // kilitlenmiþ bir hesaba yeniden giriþ yapabilmek için gereken bekleme süresi
                                                                                   //options.Lockout.AllowedForNewUsers = true; // Yeniden kayýt olmaya imkan ver
                #endregion
                options.User.RequireUniqueEmail = true; //Her email 1 kere kayýt olabilir.
                options.SignIn.RequireConfirmedEmail = false;
            });


            builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()))
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
                        fv.DisableDataAnnotationsValidation = true; 
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        options.JsonSerializerOptions.WriteIndented = true;
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddEndpointsApiExplorer();



            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalStore.WebApi", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "DigitalStore Management",
                    Description = "Enter JWT Bearer token **_only_**",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }});
            });

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new AutofacBusinessModule(builder.Configuration));
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = jwtConfig.Issuer,
                       ValidAudience = jwtConfig.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                   };
               });


            builder.Services.AddScoped<ISessionContext>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var sessionContext = new SessionContext();
                sessionContext.Session = JwtManager.GetSession(context.HttpContext);
                sessionContext.HttpContext = context.HttpContext;
                return sessionContext;
            });

            builder.Services.AddSingleton<Action<RequestProfilerModel>>(model =>
            {
                Log.Information("-------------Request-Begin------------");
                Log.Information(model.Request);
                Log.Information(Environment.NewLine);
                Log.Information(model.Response);
                Log.Information("-------------Request-End------------");
            });

            builder.Services.AddMemoryCache();
            builder.Host.UseSerilog();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.DocExpansion(DocExpansion.None);
                });
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<HeartbeatMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();
            app.UpdateDatabase();
            app.Run();
        }
    }
}