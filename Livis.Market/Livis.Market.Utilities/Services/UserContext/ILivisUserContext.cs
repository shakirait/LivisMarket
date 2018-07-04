using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Market.Utilities.Services.UserContext
{
    public interface ILivisUserContext
    {
        Task GenerateClaimIdentity(string username);
        Task<string> GetCurrentCurrency();
        Task<CustomerGroup> GetCustomerGroup();
        Task ChangePassword(string newPassword, LevisUser currentUser);
        Task<CustomerContact> GetCurrentContact();
        Task<LevisUser> GetCurrentUser();
    }
}
