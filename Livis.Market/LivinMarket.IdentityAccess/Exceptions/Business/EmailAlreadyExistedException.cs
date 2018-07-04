using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class EmailAlreadyExistedException : LivisException
    {
        public EmailAlreadyExistedException()
        {
        }

        public EmailAlreadyExistedException(string message)
            : base(message)
        {
        }

        public EmailAlreadyExistedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EmailAlreadyExistedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}