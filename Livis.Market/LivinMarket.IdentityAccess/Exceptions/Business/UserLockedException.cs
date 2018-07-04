using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class UserLockedException : LivisException
    {
        public UserLockedException()
        {
        }

        public UserLockedException(string message)
            : base(message)
        {
        }

        public UserLockedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserLockedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}