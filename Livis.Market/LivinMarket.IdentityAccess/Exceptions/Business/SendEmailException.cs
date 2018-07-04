using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class SendEmailException : LivisException
    {
        public SendEmailException()
        {
        }

        public SendEmailException(string message)
            : base(message)
        {
        }

        public SendEmailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SendEmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}