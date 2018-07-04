using Autofac;
using Autofac.Extensions.DependencyInjection;
using LivinMarket.Address.Autofac;
using LivinMarket.Contact.Autofac;
using LivinMarket.IdentityAccess.Autofac;
using LivinMarket.Order.Autofac;
using LivinMarket.Organisation.Autofac;
using LivinMarket.Payment.Autofac;
using LivinMarket.Product.Autofac;
using Livis.Common.Autofac;
using Livis.Market.Services;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Autofac;
using Livis.Market.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Livis.Market.Autofac
{
    public static class Bootstrapper
    {
        public static ContainerBuilder InitializeContainer(IServiceCollection services)
        {
            return InitializeContainerImpl(services);
        }
        
        private static ContainerBuilder InitializeContainerImpl(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            // make for each scope we will init difference instance
            builder.RegisterType<AutofacServiceProvider>()
                .As<IServiceProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterModule<LivisModule>()
                   .RegisterModule<AddressModule>()
                   .RegisterModule<ContactModule>()
                   .RegisterModule<OrderModule>()
                   .RegisterModule<PaymentModule>()
                   .RegisterModule<ProductModule>()  
                   .RegisterModule<UtilitiesModule>()
                   .RegisterModule<CommonModule>()
                   .RegisterModule<OrganisationModule>()
                   .RegisterModule<IdentityAccessModule>();
            
            builder.RegisterType<SessionService>()
                        .AsSelf()
                     .AsImplementedInterfaces()
                     .SingleInstance();
            builder.RegisterType<AppSettings>().
                    As<IAppSettings>().
                    SingleInstance();
            builder.Populate(services);
            Container.SetContainer(builder.Build());
            return builder;
        }
    }
}