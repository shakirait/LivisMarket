using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Utilities.Services.Payment
{
    public class BraintreeClientPolicy
    {
        public BraintreeClientPolicy()
        {
            this.Environment = string.Empty;
            this.MerchantId = string.Empty;
            this.PublicKey = string.Empty;
            this.PrivateKey = string.Empty;
        }

        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
