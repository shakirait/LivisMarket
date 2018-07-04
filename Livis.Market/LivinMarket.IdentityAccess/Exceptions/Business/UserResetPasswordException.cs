using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class UserResetPasswordException : LivisException
    {
        public UserResetPasswordException()
        {
        }

        public UserResetPasswordException(string message)
            : base(message)
        {
        }

        public UserResetPasswordException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserResetPasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}