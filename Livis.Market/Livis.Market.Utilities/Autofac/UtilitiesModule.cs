using Autofac;
using Livis.Market.Utilities.Serialization;
using Livis.Market.Utilities.Services.Mail;
using Livis.Market.Utilities.Services.Messaging;
//using Livis.Market.Utilities.Services.Payment;
using System.Reflection;
using Module = Autofac.Module;

namespace Livis.Market.Utilities.Autofac
{
    public class UtilitiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewtonsoftJsonSerializer>().
                   As<IJsonSerializer>().SingleInstance();
            builder.RegisterType<SendGridEmailService>().
                   As<ILivinEmailService>();
            builder.RegisterType<MessageService>().
                   As<IMessageService>();
            //builder.RegisterType<BrainTreeGatewayService>().
            //     As<ILivinPaymentService>();

        }
    }
}
