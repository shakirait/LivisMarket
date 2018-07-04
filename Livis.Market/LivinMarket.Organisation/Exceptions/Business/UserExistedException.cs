using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LivinMarket.Organisation.Exceptions.Business
{

    public class UserExistedException : LivisException
    {
        public UserExistedException()
        {
        }

        public UserExistedException(string message)
            : base(message)
        {
        }

        public UserExistedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserExistedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
