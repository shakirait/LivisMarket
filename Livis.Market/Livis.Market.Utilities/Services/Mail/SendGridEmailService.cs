using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Services.Messaging;
using SendGrid;
using System.Linq;
using SendGrid.Helpers.Mail;

namespace Livis.Market.Utilities.Services.Mail
{
    public class SendGridEmailService : ILivinEmailService
    {
        private readonly IAppSettings _appSettings;

        public SendGridEmailService(IAppSettings appSettings)
        {
            _appSettings = appSettings;

        }

        public async Task<MessageResponse> SendAsync(MailMessage message)
        {
            var sendGridResponse = await SendMailAsync(message);
            return new MessageResponse(sendGridResponse.StatusCode);
        }

        private async Task<Response> SendMailAsync(MailMessage message)
        {
            var client = new SendGridClient(_appSettings.SendGridApiKey);
            var from = new EmailAddress(message.From.Address, message.From.DisplayName);
            var subject = message.Subject;
            var to = new EmailAddress(message.To.First().Address, message.To.First().DisplayName);
            var plainTextContent = Regex.Replace(message.Body, "<[^>]*>", "");
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }
    }
}