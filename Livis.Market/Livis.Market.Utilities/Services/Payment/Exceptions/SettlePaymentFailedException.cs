using System;
using System.Runtime.Serialization;

namespace Livis.Market.Utilities.Services.Payment.Exceptions
{
    public class SettlePaymentFailedException : Exception
    {
        public SettlePaymentFailedException()
        {
        }

        public  SettlePaymentFailedException(string message)
            : base(message)
        {
        }

        public  SettlePaymentFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SettlePaymentFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}