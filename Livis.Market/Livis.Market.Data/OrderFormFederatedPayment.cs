using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Data
{
    public class OrderFormFederatedPayment : OrderForm
    {
        public string CardType { get; set; }
        public string EscrowId { get; set; }
        public int ExpiresMonth { get; set; }
        public int ExpiresYear { get; set; }
        public string MaskedNumber { get; set; }
        public string PaymentMethodNonce { get; set; }
        public string TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public Party BillingParty { get; set; }
    }
}
