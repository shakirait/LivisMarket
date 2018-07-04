using System;
using System.Runtime.Serialization;

namespace Livis.Market.Infrastructure
{
    [Serializable]
    public class LivisException : Exception
    {
        public LivisException()
        {

        }

        public LivisException(string message)
           : base(message)
        {
        }

        public LivisException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected LivisException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
