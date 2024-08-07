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

            // UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();


            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });

            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance(); 

            builder.RegisterAssemblyTypes(typeof(CreateCategoryCommand).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope(); 

            // MediatR
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            //builder.Register<ServiceFactory>(context =>
            //{
            //    var c = context.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //}).InstancePerLifetimeScope(); 


        }
    }
    //// Tüm servislerin register edildimesi.
    //protected override void Load(ContainerBuilder builder)
    //{
    //    builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
    //    builder.RegisterType<UnitOfWork<Product>>().As<IUnitOfWork<Product>>().SingleInstance();
    //    builder.RegisterType<UnitOfWork<Order>>().As<IUnitOfWork<Order>>().SingleInstance();
    //    builder.RegisterType<UnitOfWork<OrderDetail>>().As<IUnitOfWork<OrderDetail>>().SingleInstance();
    //    builder.RegisterType<UnitOfWork<User>>().As<IUnitOfWork<User>>().SingleInstance();
    //    builder.RegisterType<UnitOfWork<Category>>().As<IUnitOfWork<Category>>().SingleInstance();
    //    builder.RegisterType<UnitOfWork<ProductCategory>>().As<IUnitOfWork<ProductCategory>>().SingleInstance();

    //}
}

