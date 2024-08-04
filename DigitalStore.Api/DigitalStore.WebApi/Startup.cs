using Autofac;
using Autofac.Core;
using DigitalStore.Base;
using DigitalStore.Base.Token;
using DigitalStore.Business.IdentityService;
using DigitalStore.Data.Context;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                services.AddDbContext<StoreIdentityDbContext> (options => options.UseSqlServer(connectionStringSql));
            }
            else
            {
                var connectionStringPostgre = Configuration.GetConnectionString("PostgresSqlConnection");
                services.AddDbContext<StoreIdentityDbContext>(options => options.UseNpgsql(connectionStringPostgre));
            }


            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
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


            //var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            //services.AddSingleton(config.CreateMapper());


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                    ValidAudience = jwtConfig.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
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

            services.AddMemoryCache();

            //var redisConfig = new ConfigurationOptions();
            //redisConfig.DefaultDatabase = 0;
            //redisConfig.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
            //services.AddStackExchangeRedisCache(opt =>
            //{
            //    opt.ConfigurationOptions = redisConfig;
            //    opt.InstanceName = Configuration["Redis:InstanceName"];
            //});

            services.AddScoped<ISessionContext>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var sessionContext = new SessionContext();
                sessionContext.Session = JwtManager.GetSession(context.HttpContext);
                sessionContext.HttpContext = context.HttpContext;
                return sessionContext;
            });
        }

        public class AutofacBusinessModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
                builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            }
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalStore.WebApi v1"));
            }


            //app.UseMiddleware<HeartbeatMiddleware>();
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            //Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
            //{
            //    Log.Information("-------------Request-Begin------------");
            //    Log.Information(requestProfilerModel.Request);
            //    Log.Information(Environment.NewLine);
            //    Log.Information(requestProfilerModel.Response);
            //    Log.Information("-------------Request-End------------");
            //};
            //app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

            //app.UseHangfireDashboard();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
