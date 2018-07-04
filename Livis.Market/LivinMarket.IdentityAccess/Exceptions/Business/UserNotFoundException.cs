using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class UserNotFoundException :LivisException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}