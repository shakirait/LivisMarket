using Autofac;
using LivinMarket.Payment.Model.Queries.Business;
using Livis.Market.Autofac;
using Livis.Market.Caching;
using Livis.Market.Infrastructure;
using Livis.Market.Postprocessing;
using Livis.Market.Preprocessing;
using Livis.Market.Validation;
using System.Reflection;
using Module = Autofac.Module;

namespace LivinMarket.Payment.Autofac
{
    public class PaymentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var targetAssembly = Assembly.GetAssembly(typeof(PaymentQuery));

            builder
                .RegisterAssemblyTypes(targetAssembly)
                .AsClosedTypesOf(typeof(IPreprocessor<>))
                .As<IPreprocessor>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(targetAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .As<IValidator>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(targetAssembly)
                .BasedOn(typeof(IValidationRule<>))
                .AsSelf()
                .As<IValidationRule>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(targetAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(IRequestHandler<,>),
                    serviceKey: "requestHandler")
                .As<IRequestHandler>()
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestValidationDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestHandler",
                    toKey: "requestValidation")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestPreprocessingDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestValidation",
                    toKey: "requestPreprocessing")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestPostprocessingDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestPreprocessing",
                    toKey: "requestPostprocessing")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestCachingDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestPostprocessing")
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(targetAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(ICommandHandler<>),
                    serviceKey: "commandHandler")
                .As<ICommandHandler>()
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(CommandValidationDecorator<>),
                    decoratedServiceType: typeof(ICommandHandler<>),
                    fromKey: "commandHandler",
                    toKey: "commandValidation")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(CommandPreprocessingDecorator<>),
                    decoratedServiceType: typeof(ICommandHandler<>),
                    fromKey: "commandValidation")
                .InstancePerLifetimeScope();
        }
    }
}