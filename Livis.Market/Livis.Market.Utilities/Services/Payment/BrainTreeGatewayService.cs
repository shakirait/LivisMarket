using Braintree;
using Braintree.Exceptions;
using Livis.Market.Data;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Services.Payment.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Livis.Market.Utilities.Helper;

namespace Livis.Market.Utilities.Services.Payment
{
    public class BrainTreeGatewayService : ILivinPaymentService
    {
        private BraintreeClientPolicy _clientPolicy;
        private readonly IAppSettings _appSettings;
        private readonly LivisMarketContext _livisMarketContext;
        public BrainTreeGatewayService(IAppSettings appSettings, LivisMarketContext livisMarketContext)
        {
            _appSettings = appSettings;
            _livisMarketContext = livisMarketContext;
        }

        private BraintreeClientPolicy ClientPolicy
        {
            get
            {
                if (_clientPolicy == null)
                {
                    if (string.IsNullOrEmpty(_appSettings.Environment) ||                      string.IsNullOrEmpty(_appSettings.MerchantId) || 
                        string.IsNullOrEmpty(_appSettings.PublicKey) || 
                        string.IsNullOrEmpty(_appSettings.PrivateKey))
                    {
                        throw new InvalidClientPolicyException("Invalid BraintreeClientPolicy");
                    }
                    _clientPolicy = new BraintreeClientPolicy()
                    {
                       Environment = _appSettings.Environment,
                       MerchantId = _appSettings.MerchantId,
                       PrivateKey = _appSettings.PrivateKey,
                       PublicKey = _appSettings.PublicKey
                    };
                }
                return _clientPolicy;
            }
        }

        public async Task<string> GetClientToken()
        {
            try
            {
                var gateway = new BraintreeGateway(_clientPolicy.Environment, _clientPolicy.MerchantId, _clientPolicy.PublicKey, _clientPolicy.PrivateKey);
                var clientToken = await gateway.ClientToken.GenerateAsync();
                return clientToken;
            }
            catch (BraintreeException ex)
            {
                throw new InvalidClientPolicyException("Invalid BraintreeClientPolicy", ex);
            }
        }

        public async Task<OrderFormFederatedPayment> CreateFederatedPayment(SerializableCart cart)
        {
            if(cart == null)
            {
                throw new InvalidOrMissingPropertyValueException("The cart can not be null");
            }
            var payment = _livisMarketContext.OrderFormsFederatedPayment.FirstOrDefault(x =>    x.CartId.HasValue && x.CartId.Value.Equals(cart.CartId));
            if (payment == null)
            {
                throw new InvalidOrMissingPropertyValueException("The payment can not be null");
            }
            if (string.IsNullOrEmpty(payment.PaymentMethodNonce))
            {
                throw new InvalidOrMissingPropertyValueException("Invalid or missing value for property 'PaymentMethodNonce'.");
            }

            try
            {
                var gateway = new BraintreeGateway(_clientPolicy.Environment, _clientPolicy.MerchantId, _clientPolicy.PublicKey, _clientPolicy.PrivateKey);
                var request = new TransactionRequest
                {
                    Amount = payment.Amount,
                    PaymentMethodNonce = payment.PaymentMethodNonce,
                    BillingAddress = ComponentsHelper.TranslatePartyToAddressRequest(payment.BillingParty),
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = false
                    }
                };
                Result<Transaction> result = gateway.Transaction.Sale(request);

                if (result.IsSuccess())
                {
                    Transaction transaction = result.Target;
                    payment.TransactionId = transaction?.Id;
                    payment.TransactionStatus = transaction?.Status?.ToString();
                    //payment.PaymentInstrumentType = transaction?.PaymentInstrumentType?.ToString();
                    var cc = transaction?.CreditCard;
                    payment.MaskedNumber = cc?.MaskedNumber;
                    payment.CardType = cc?.CardType?.ToString();
                    if (cc?.ExpirationMonth != null)
                    {
                        payment.ExpiresMonth = int.Parse(cc.ExpirationMonth);
                    }

                    if (cc?.ExpirationYear != null)
                    {
                        payment.ExpiresYear = int.Parse(cc.ExpirationYear);
                    }
                    await _livisMarketContext.SaveChangesAsync();
                    return payment;
                }
                else
                {
                    string errorMessages = result.Errors.DeepAll().Aggregate(string.Empty, (current, error) => current + ("Error: " + (int)error.Code + " - " + error.Message + "\n"));
                    throw new CreatePaymentFailedException(errorMessages);
                }
            }
            catch (BraintreeException ex)
            {
                throw new CreatePaymentFailedException("Create payment failed. " + ex.Message, ex);
            }
        }

        public async Task<Data.FederatedPayment> EnsureSettlePaymentRequested(Cart cart)
        {
            if (cart == null)
            {
                throw new InvalidOrMissingPropertyValueException("The cart can not be null");
            }
            var payment = _livisMarketContext.FederatedPayments.FirstOrDefault(x => x.CartId.HasValue && x.CartId.Value.Equals(cart.CartId));
            if (payment == null)
            {
                throw new InvalidOrMissingPropertyValueException("The payment can not be null");
            }
            if (string.IsNullOrEmpty(payment.TransactionId))
            {
                throw new InvalidOrMissingPropertyValueException($"Invalid or missing value for property 'TransactionId'.");
            }
            try
            {
                var gateway = new BraintreeGateway(_clientPolicy.Environment, _clientPolicy.MerchantId, _clientPolicy.PublicKey, _clientPolicy.PrivateKey);

                var transaction = gateway.Transaction.Find(payment.TransactionId);
                if (transaction.Status.ToString().Equals("authorized", StringComparison.OrdinalIgnoreCase))
                {
                    var result = gateway.Transaction.SubmitForSettlement(payment.TransactionId, payment.Amount);

                    if (result.IsSuccess())
                    {
                        var settledTransaction = result.Target;
                        payment.TransactionStatus = settledTransaction.Status.ToString();

                        // Force settlement for testing
                        if (_clientPolicy.Environment.Equals("sandbox", StringComparison.OrdinalIgnoreCase))
                        {
                            gateway.TestTransaction.Settle(payment.TransactionId);
                            settledTransaction = gateway.Transaction.Find(payment.TransactionId);
                            payment.TransactionStatus = settledTransaction.Status.ToString();
                        }
                        await _livisMarketContext.SaveChangesAsync();
                        return payment;
                    }
                    else
                    {
                        var errorMessages = result.Errors.DeepAll().Aggregate(string.Empty, (current, error) => current + ("Error: " + (int)error.Code + " - " + error.Message + "\n"));
                        throw new SettlePaymentFailedException($"Settle payment failed for { payment.TransactionId }: { errorMessages }");
                    }
                }
                return payment;
            }
            catch (BraintreeException ex)
            {
                throw new SettlePaymentFailedException($"Settle payment failed for { payment.TransactionId } { ex.Message }", ex);
            }
        }

        public async Task<Data.FederatedPayment> ValidateSettlement(Cart cart)
        {
            if (cart == null)
            {
                throw new InvalidOrMissingPropertyValueException("The cart can not be null");
            }
            var payment = _livisMarketContext.FederatedPayments.FirstOrDefault(x => x.CartId.HasValue && x.CartId.Value.Equals(cart.CartId));
            if (payment == null)
            {
                throw new InvalidOrMissingPropertyValueException("The payment can not be null");
            }
            if (string.IsNullOrEmpty(payment.TransactionId))
            {
                throw new InvalidOrMissingPropertyValueException($"Invalid or missing value for property 'TransactionId'.");
            }
            try
            {
                var gateway = new BraintreeGateway(_clientPolicy.Environment, _clientPolicy.MerchantId, _clientPolicy.PublicKey, _clientPolicy.PrivateKey);

                var transaction = gateway.Transaction.Find(payment.TransactionId);
               
                switch (transaction.Status.ToString())
                {
                    case "settled":
                        payment.TransactionStatus = transaction.Status.ToString();
                        break;
                    case "submitted_for_settlement":
                        payment.TransactionStatus = "pending";
                        break;
                    default:
                        payment.TransactionStatus = transaction.Status.ToString();
                        break;
                }
                await _livisMarketContext.SaveChangesAsync();
                return payment;
            }
            catch (BraintreeException ex)
            {
                throw new SettlePaymentFailedException($"Settle payment failed for { payment.TransactionId } { ex.Message }", ex);
            }
        }
    }
}
