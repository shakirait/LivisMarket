using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class PasswordNotMatchException : LivisException
    { 
        public PasswordNotMatchException()
        {
        }

        public PasswordNotMatchException(string message)
            : base(message)
        {
        }

        public PasswordNotMatchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PasswordNotMatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}