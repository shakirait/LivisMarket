
using System;
using System.Runtime.Serialization;

namespace Livis.Market.Utilities.Services.Payment.Exceptions
{
    public class InvalidOrMissingPropertyValueException : Exception
    {
        public InvalidOrMissingPropertyValueException()
        {
        }

        public InvalidOrMissingPropertyValueException(string message)
            : base(message)
        {
        }

        public InvalidOrMissingPropertyValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidOrMissingPropertyValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

