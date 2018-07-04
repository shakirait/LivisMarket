using Autofac;
using Livis.Common.Notifications;
using Livis.Market.Utilities.Serialization;
using Livis.Market.Utilities.Services.Mail;
using Livis.Market.Utilities.Services.Messaging;
using System.Reflection;
using Module = Autofac.Module;

namespace Livis.Common.Autofac
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationService>().
                   As<INotificationService>().SingleInstance();
        }
    }
}
