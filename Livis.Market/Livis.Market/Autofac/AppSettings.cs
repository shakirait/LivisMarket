using Livis.Market.Utilities.ApplicationSettings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Autofac
{
    public class AppSettings : IAppSettings
    {
        #region Constant Keys
        private const string _SENDGRID_APIKEY_NOT_FOUND = "SendGrid:ApiKey was not found in appsettings.json!";
        private const string _SENDGRID_APIKEY_EMPTY = "SendGrid:ApiKey is empty in appsettings.json!";
        private const string _SEND_GRID_API = "SendGrid";
        private const string _SEND_GRID_API_KEY_KEY = "SendGrid:ApiKey";
        private const string _EMAIL_ADDRESS_KEY = "EmailAddress";
        private const string _EMAIL_DISPLAY_NAME_KEY = "EmailDisplayName";
        private const string _DEFAULT_EMAIL_ADDRESS = "tnguyen317@icloud.com";
        private const string _DEFAULT_EMAIL_DISPLAY_NAME = "Livin Rose";
        private const string _BRAIN_TREE_ENV = "BrainTree:Environment";
        private const string _BRAIN_TREE_MER = "BrainTree:MerchantId";
        private const string _BRAIN_TREE_PUB = "BrainTree:PublicKey";
        private const string _BRAIN_TREE_PRI = "BrainTree:PrivateKey";
        private const string _DEFAULT_CURRENCY = "DefaultCurrency";
        private const string _DEFAULT_LANGUAGE = "DefaultLanguage";
        private const string _DEFAULT_SHOP_NAME = "DefaultShopName";
        private const string _DEFAUT_MARKET_NAME = "DefaultMarketName";
        #endregion
        #region Private 
        private readonly IConfiguration _configuration;
        private string _sendGridApiKey;
        private string _emailAddress;
        private string _emailDisplayName;
        private string _environment;
        private string _merchantId;
        private string _publicKey;
        private string _privateKey;
        private string _defaultCurrency;
        private string _defaultLanguage;
        private string _defaultShopName;
        private string _defaultMarketName;
        #endregion

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string SendGridApiKey
        {
            get
            {
                if (_sendGridApiKey != null)
                {
                    return _sendGridApiKey;
                }
                var hasSendGridApiKey = _configuration.GetChildren().Any(x => x.Key == _SEND_GRID_API);

                if (!hasSendGridApiKey)
                {
                    throw new KeyNotFoundException(_SENDGRID_APIKEY_NOT_FOUND);
                }

                var value = _configuration[_SEND_GRID_API_KEY_KEY];

                if (string.IsNullOrEmpty(value))
                {
                    throw new KeyNotFoundException(_SENDGRID_APIKEY_EMPTY);
                }

                return _sendGridApiKey = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                if (_emailAddress != null)
                {
                    return _emailAddress;
                }

                var value = _configuration[_EMAIL_ADDRESS_KEY];

                return string.IsNullOrEmpty(value)
                    ? (_emailAddress = _DEFAULT_EMAIL_ADDRESS)
                    : (_emailAddress = value);
            }
        }

        public string EmailDisplayName
        {
            get
            {
                if (_emailDisplayName != null)
                {
                    return _emailDisplayName;
                }

                var value = _configuration[_EMAIL_DISPLAY_NAME_KEY];

                return string.IsNullOrEmpty(value)
                    ? (_emailDisplayName = _DEFAULT_EMAIL_DISPLAY_NAME)
                    : (_emailDisplayName = value);
            }
        }

        public string Environment
        {
            get
            {
                if (_environment != null)
                {
                    return _environment;
                }

                var value = _configuration[_BRAIN_TREE_ENV];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_environment = value);
            }
        }

        public string MerchantId
        {
            get
            {
                if (_merchantId != null)
                {
                    return _merchantId;
                }

                var value = _configuration[_BRAIN_TREE_MER];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_merchantId = value);
            }
        }

        public string PublicKey
        {
            get
            {
                if (_publicKey != null)
                {
                    return _publicKey;
                }

                var value = _configuration[_BRAIN_TREE_PUB];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_publicKey = value);
            }
        }

        public string PrivateKey
        {
            get
            {
                if (_privateKey != null)
                {
                    return _privateKey;
                }

                var value = _configuration[_BRAIN_TREE_PRI];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_privateKey = value);
            }
        }

        public string DefaultCurrency
        {
            get {
                if (_defaultCurrency != null)
                {
                    return _defaultCurrency;
                }
                var value = _configuration[_DEFAULT_CURRENCY];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_defaultCurrency = value);
            }
        }

        public string DefaultLanguage
        {
            get
            {
                if (_defaultLanguage != null)
                {
                    return _defaultLanguage;
                }
                var value = _configuration[_DEFAULT_LANGUAGE];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_defaultLanguage = value);
            }
        }

        public string DefaultShopName {
            get {
                if (_defaultShopName != null)
                {
                    return _defaultShopName;
                }
                var value = _configuration[_DEFAULT_SHOP_NAME];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_defaultShopName = value);
            }
        }

        public string DefaultMartketName {
            get
            {
                if (_defaultMarketName != null)
                {
                    return _defaultMarketName;
                }
                var value = _configuration[_DEFAUT_MARKET_NAME];

                return string.IsNullOrEmpty(value)
                    ? string.Empty
                    : (_defaultMarketName = value);
            }
        }
    }
}
