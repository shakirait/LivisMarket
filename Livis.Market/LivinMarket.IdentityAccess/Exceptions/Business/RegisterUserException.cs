using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class RegisterUserException : LivisException
    {
        public RegisterUserException()
        {
        }

        public RegisterUserException(string message)
            : base(message)
        {
        }

        public RegisterUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RegisterUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
