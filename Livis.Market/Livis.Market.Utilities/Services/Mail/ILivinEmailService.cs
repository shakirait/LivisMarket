using Livis.Market.Utilities.Services.Messaging;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Livis.Market.Utilities.Services.Mail
{
    public interface ILivinEmailService
    {
        Task<MessageResponse> SendAsync(MailMessage message);
    }
}
