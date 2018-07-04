using Braintree;
using Livis.Market.Data;
using System.Collections.Generic;

namespace Livis.Market.Utilities.Helper
{
    public class ComponentsHelper
    {
        #region Currency
        public static Dictionary<string, string> Currency = new Dictionary<string, string>() {
             {"USD", "$"},
             {"CAD", "$"},
             {"EUR", "€"},
             {"GBP", "£"},
             {"AUD", "$"}
        };
        #endregion
        #region Status Registration For Organisation
        public static string Provisional = "Provisional";
        public static string Completed = "Completed";
        public static string FullyRegisted = "FullyRegisted";
        public static string Rejected = "Rejected";
        #endregion
        #region Customer Group
        public static string Customer = "Customer";
        public static string Admin = "Admin";
        public static string Agency = "Agency";
        public static string Supplier = "Supplier";
        #endregion
        public static AddressRequest TranslatePartyToAddressRequest(Party party)
        {
            var addressRequest = new AddressRequest();
            addressRequest.CountryCodeAlpha2 = party.CountryCode;
            addressRequest.CountryName = party.Country;
            addressRequest.FirstName = party.FirstName;
            addressRequest.LastName = party.LastName;
            addressRequest.PostalCode = party.ZipPostalCode;
            addressRequest.StreetAddress = string.Concat(party.Address1, ",", party.Address2);

            return addressRequest;
        }

        public static Dictionary<string, string> GetRegistrationStatusList()
        {
            var status = new Dictionary<string, string>();
            status.Add(Provisional, "Provisional");
            status.Add(Completed, "Completed");
            status.Add(FullyRegisted, "Fully Registed");
            status.Add(Rejected, "Rejected");

            return status;
        }

        public static Dictionary<string, string> GetCountryList()
        {
            var countries = new Dictionary<string, string>();
            countries.Add("DE", "Germany");

            return countries;
        }
    }
}
