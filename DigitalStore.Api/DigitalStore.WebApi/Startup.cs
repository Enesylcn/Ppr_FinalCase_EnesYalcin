using Autofac;
using Autofac.Core;
using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Token;
using DigitalStore.Business.Application.CategoryOperations.Commands.CreateCategory;
using DigitalStore.Business.IdentityService;
using DigitalStore.Business.Mapper;
using DigitalStore.Data.Context;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.WebApi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Para.Api;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitalStore.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration;
        public static JwtConfig jwtConfig { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddSingleton<JwtConfig>(jwtConfig);

            string type = Configuration.GetConnectionString("DbType");
            if (type == "MsSql")
            {
                var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContext<StoreIdentityDbContext>(options => options.UseSqlServer(connectionStringSql));
            }
            else
            {
                var connectionStringPostgre = Configuration.GetConnectionString("PostgresSqlConnection");
                services.AddDbContext<StoreIdentityDbContext>(options => options.UseNpgsql(connectionStringPostgre));
            }


            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                #region Password Options
                options.Password.RequiredLength = 6;// Parola en az 6 karakter olmalı
                options.Password.RequireDigit = true;//Parola en az 1 adet sayısal değer içermeli
                options.Password.RequireNonAlphanumeric = true;//Parola özel karakter içermeli
                options.Password.RequireUppercase = true;// Parola büyük harf içermeli
                options.Password.RequireLowercase = true;// Parola küçük harf içermeli
                                                         //options.Password.RequiredUniqueChars = // tekrar etmemesi istenilen karakterleri dizi şeklinde verip kullanılıyor. 
                #endregion
                #region Hesap Kilitleme Ayarları
                options.Lockout.MaxFailedAccessAttempts = 3; // Üst üste hatalı giriş denemesi
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15); // kilitlenmiş bir hesaba yeniden giriş yapabilmek için gereken bekleme süresi
                                                                                   //options.Lockout.AllowedForNewUsers = true; // Yeniden kayıt olmaya imkan ver
                #endregion
                options.User.RequireUniqueEmail = true; //Her email 1 kere kayıt olabilir.
                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //services.AddControllers().AddFluentValidation(x =>
            //{
            //    x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
            //});


            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            services.AddSingleton(config.CreateMapper());

            services.AddMediatR(typeof(CreateCategoryCommandHandler).Assembly);
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = true;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = jwtConfig.Issuer,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
            //        ValidAudience = jwtConfig.Audience,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.FromMinutes(2)

            //    };
            //});

            services.AddAuthentication(options =>
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


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalStore.WebApi Management", Version = "v1.0" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "DigitalStore Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
            });

            //services.AddMemoryCache();

            services.AddScoped<ISessionContext>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var sessionContext = new SessionContext();
                sessionContext.Session = JwtManager.GetSession(context.HttpContext);
                sessionContext.HttpContext = context.HttpContext;
                return sessionContext;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalStore.WebApi v1"));
            }


            app.UseMiddleware<HeartbeatMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            //Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
            //{
            //    logger.LogInformation("-------------Request-Begin------------");
            //    logger.LogInformation(requestProfilerModel.Request);
            //    logger.LogInformation(Environment.NewLine);
            //    logger.LogInformation(requestProfilerModel.Response);
            //    logger.LogInformation("-------------Request-End------------");
            //};
            //app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

            //app.UseHangfireDashboard();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            logger.LogInformation("Application started.");
        }
    }
}
