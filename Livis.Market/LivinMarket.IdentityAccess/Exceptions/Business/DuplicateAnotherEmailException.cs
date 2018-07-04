using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LivinMarket.IdentityAccess.Exceptions.Business
{
    public class DuplicateAnotherEmailException : LivisException
    {
        public DuplicateAnotherEmailException()
        {
        }

        public DuplicateAnotherEmailException(string message) : base(message)
        {
        }

        protected DuplicateAnotherEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DuplicateAnotherEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
