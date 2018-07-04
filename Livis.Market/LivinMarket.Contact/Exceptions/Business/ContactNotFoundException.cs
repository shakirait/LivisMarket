using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LivinMarket.Contact.Exceptions.Business
{
    public class ContactNotFoundException : LivisException
    {
        public ContactNotFoundException()
        {
        }

        public ContactNotFoundException(string message)
            : base(message)
        {
        }

        public ContactNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ContactNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}