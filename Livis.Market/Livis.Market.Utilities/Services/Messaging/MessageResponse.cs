using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Livis.Market.Utilities.Services.Messaging
{
    public class MessageResponse
    {
        public MessageResponse(HttpStatusCode status)
        {
            Status = status;
        }

        public HttpStatusCode Status { get; }
    }
}
