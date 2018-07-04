using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaymentGateway = Livis.Market.Data.Payment;

namespace Livis.Market.Utilities.Services.Payment
{
    public interface ILivinPaymentService
    {
        Task<string> GetClientToken();
        Task<Data.FederatedPayment> CreateFederatedPayment(Cart cart);
        Task<Data.FederatedPayment> EnsureSettlePaymentRequested(Cart cart);
        Task<Data.FederatedPayment> ValidateSettlement(Cart cart);
    }
}
