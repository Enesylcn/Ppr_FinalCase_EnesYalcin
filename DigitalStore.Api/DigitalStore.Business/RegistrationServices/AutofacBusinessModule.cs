using Autofac;
using AutoMapper;
using DigitalStore.Business.Application.CategoryOperations.Commands.CreateCategory;
using DigitalStore.Business.IdentityService;
using DigitalStore.Business.Mapper;
using DigitalStore.Data.Context;
using DigitalStore.Data.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DigitalStore.Business.RegistrationServices
{
    public class AutofacBusinessModule : Module
    {
        private readonly IConfiguration _configuration;

        public AutofacBusinessModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionStringMsSql = _configuration.GetConnectionString("MsSqlConnection");

            builder.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<StoreIdentityDbContext>();
                optionsBuilder.UseSqlServer(connectionStringMsSql);
                return new StoreIdentityDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });

            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance(); 

            builder.RegisterAssemblyTypes(typeof(CreateCategoryCommand).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope(); 

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();


        }
    }
}

