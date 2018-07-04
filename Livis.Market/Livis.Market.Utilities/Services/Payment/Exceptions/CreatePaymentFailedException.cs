
using System;
using System.Runtime.Serialization;

namespace Livis.Market.Utilities.Services.Payment.Exceptions
{
    public class CreatePaymentFailedException : Exception
    {
        public CreatePaymentFailedException()
        {
        }

        public CreatePaymentFailedException(string message)
            : base(message)
        {
        }

        public CreatePaymentFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CreatePaymentFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}