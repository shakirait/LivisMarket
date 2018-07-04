using System;
using System.Runtime.Serialization;

namespace Livis.Market.Utilities.Services.Payment.Exceptions
{
    public class InvalidClientPolicyException : Exception
    { 
        public InvalidClientPolicyException()
        {
        }

        public InvalidClientPolicyException(string message)
            : base(message)
        {
        }

        public InvalidClientPolicyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidClientPolicyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

