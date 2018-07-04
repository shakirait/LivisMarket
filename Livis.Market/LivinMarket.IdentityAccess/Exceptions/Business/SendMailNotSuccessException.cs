using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class SendMailNotSuccessException : LivisException
    {
        public SendMailNotSuccessException()
        {
        }

        public SendMailNotSuccessException(string message)
            : base(message)
        {
        }

        public SendMailNotSuccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SendMailNotSuccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}