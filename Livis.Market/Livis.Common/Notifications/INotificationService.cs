using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Common.Notifications
{
    public interface INotificationService
    {
        Task<bool> ForgotPasswordAsync(string username, string password);
        Task<bool> AgencyConfirmationAsync(string confirmationLink, string username, string password);
        Task<bool> ThankYouAgencyConfirmationAsync(string username);
        Task<bool> AnnounceAdminAgencyCompletedAsync(string username);
        Task<bool> AnnounceChangePasswordAsync(string username);
    }
}
