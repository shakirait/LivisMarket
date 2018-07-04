using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Market.Utilities.Services.Messaging
{
    public interface IMessageService
    {
        Task<MessageResponse> SendAsync(IMessageContent message, MessageType messageType = MessageType.Email);
    }
}
