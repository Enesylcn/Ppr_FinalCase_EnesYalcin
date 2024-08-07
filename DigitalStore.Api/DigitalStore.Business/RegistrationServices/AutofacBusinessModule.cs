using Autofac;
using DigitalStore.Business.IdentityService;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;

namespace DigitalStore.Business.RegistrationServices
{
    public class AutofacBusinessModule : Module
    {
        // Tüm servislerin register edildimesi.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork<Product>>().As<IUnitOfWork<Product>>().SingleInstance();
            builder.RegisterType<UnitOfWork<Order>>().As<IUnitOfWork<Order>>().SingleInstance();
            builder.RegisterType<UnitOfWork<OrderDetail>>().As<IUnitOfWork<OrderDetail>>().SingleInstance();
            builder.RegisterType<UnitOfWork<User>>().As<IUnitOfWork<User>>().SingleInstance();
            builder.RegisterType<UnitOfWork<Category>>().As<IUnitOfWork<Category>>().SingleInstance();
            builder.RegisterType<UnitOfWork<ProductCategory>>().As<IUnitOfWork<ProductCategory>>().SingleInstance();

        }
    }
}
