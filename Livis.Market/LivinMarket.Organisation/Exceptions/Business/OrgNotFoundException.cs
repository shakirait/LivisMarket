using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivinMarket.Organisation.Exceptions.Business
{
    public class OrgNotFoundException :LivisException
    {
        public OrgNotFoundException()
        {
        }

        public OrgNotFoundException(string message)
            : base(message)
        {
        }

        public OrgNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected OrgNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}