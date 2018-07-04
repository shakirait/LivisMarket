using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Utilities.ApplicationSettings
{
    public interface IAppSettings
    {
        string SendGridApiKey { get; }
        string EmailAddress { get; }
        string EmailDisplayName { get; }
        string Environment { get; }
        string MerchantId { get; }
        string PublicKey { get; }
        string PrivateKey { get; }
        string DefaultCurrency { get; }
        string DefaultLanguage { get; }
        string DefaultShopName { get; }
        string DefaultMartketName { get; }
    }
}
