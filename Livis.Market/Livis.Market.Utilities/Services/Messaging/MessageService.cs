using Livis.Market.Utilities.Services.Mail;
using System.Threading.Tasks;

namespace Livis.Market.Utilities.Services.Messaging
{
    public class MessageService : IMessageService
    {
        private readonly ILivinEmailService _email;

        public MessageService(
            ILivinEmailService email)
        {
            _email = email;
        }

        public async Task<MessageResponse> SendAsync(IMessageContent message, MessageType messageType = MessageType.Email)
        {
            switch (messageType)
            {
                case MessageType.Email:
                    return await _email.SendAsync(message as MailMessage);
            }

            return null;
        }
    }
}
