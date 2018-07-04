using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.Organisation.Exceptions.Business
{
    public class UnableCreateUserException : LivisException
    {
        public UnableCreateUserException()
        {
        }

        public UnableCreateUserException(string message)
            : base(message)
        {
        }

        public UnableCreateUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnableCreateUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}