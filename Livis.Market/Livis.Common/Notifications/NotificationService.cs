using Livis.Common.Password;
using Livis.Market.Data;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Serialization;
using Livis.Market.Utilities.Services.Mail;
using Livis.Market.Utilities.Services.Messaging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Common.Notifications
{
    public class NotificationService : INotificationService
    {
        private const string _DEFAULT_FORGOTPASSWORD_ACCOUNT_REGISTRATION_EMAIL_TEMPLATE = "Dear {FullName}, {rtn}{rtn} You have just requested your password to be reset. The new password: {rtn}{rtn} {NewPass}";
        private const string _DEFAULT_FORGOTPASSWORD_EMAIL_SUBJECT = "Password Recovery";
        private const string _DEFAULT_CONFIRMATION_REGISTRATION_EMAIL_TEMPLATE = "Dear {FullName}, {rtn}{rtn} Please click the link below to activate your Livin Market account and complete your information {rtn}{rtn} {Link} {rtn}{rtn} Your temporary password: {TempPassword}";
        private const string _DEFAULT_CONFIRMATION_REGISTRATION_EMAIL_SUBJECT = "Activate Your Livin Market Account";
        private const string _DEFAULT_CONFIRMATION_THANK_EMAIL_TEMPLATE = "Dear {FullName}, {rtn}{rtn} Thank you for using our service. Now you can order any product on Livin Market site. We always provide the best deal to you.";
        private const string _DEFAULT_CONFIRMATION_THANK_EMAIL_SUBJECT = "Thank you for using Livin Market service";
        private const string _DEFAULT_AGENCY_COMPLETED_EMAIL_TEMPLATE = "Dear {FullName}, {rtn}{rtn} Agency ({Agency}) has confirmed registration form. You need to re-verify information again and switch status of agency to Fully Registed .";
        private const string _DEFAULT_AGENCY_COMPLETED_EMAIL_SUBJECT = "Agency confirmed registration information";
        private const string _DEFAULT_CHANGEPASSWORD_EMAIL_TEMPLATE = "Dear {FullName}, {rtn}{rtn} You have just change your password.";
        private const string _DEFAULT_CHANGEPASSWORD_EMAIL_SUBJECT = "Password Change";

        private readonly IAppSettings _appSettings;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IMessageService _messageService;
        private readonly UserManager<LevisUser> _userManager;
        

        public NotificationService(
            IAppSettings appSettings,
            IJsonSerializer jsonSerializer,
            IMessageService messageService,
            UserManager<LevisUser> userManager)
        {
            _appSettings = appSettings;
            _jsonSerializer = jsonSerializer;
            _messageService = messageService;
            _userManager = userManager;
        }
        #region Private Methods
        private async Task<string> generatePassword()
        {
            var generatedPassword = PasswordTool.Generate(PasswordConfiguration.LengthOfPassword, PasswordConfiguration.NumberOfNonAlphanumericCharacters);
            return generatedPassword;
        }

        private async Task<IMessageContent> SendApprovalNotification(string email, string mailSubject, string mailBody, string displayName = null)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_appSettings.EmailAddress, _appSettings.EmailDisplayName),
                To = new MailAddressCollection
                    {
                        new MailAddress(email, displayName),
                    },
                Subject = mailSubject,
                Body = mailBody,
            };
            var respone = await _messageService.SendAsync(message);
            var result = respone.Status == HttpStatusCode.Accepted;

            if (!result)
            {
                //TODO: Thanh.Nq will implement log module
                // _logger.Error("Send mail fail. " + _jsonSerializer.Serialize(message));

                return null;
            }
            return message;
        }
        #endregion
        public async Task<bool> ForgotPasswordAsync(string username, string password)
        {
            var mailTemplate = _DEFAULT_FORGOTPASSWORD_ACCOUNT_REGISTRATION_EMAIL_TEMPLATE;
            var mailSubject = _DEFAULT_FORGOTPASSWORD_EMAIL_SUBJECT;
            var mailBody = mailTemplate.Replace("{FullName}", username)
                .Replace("{rtn}", "<br/>")
                .Replace("{NewPass}", password);

            var sendResult = await SendApprovalNotification(username, mailSubject, mailBody);
            return sendResult != null;
        }

        public async Task<bool> AgencyConfirmationAsync(string confirmationLink, string username, string password)
        {
            try
            {
                var mailTemplate = _DEFAULT_CONFIRMATION_REGISTRATION_EMAIL_TEMPLATE;
                var mailSubject = _DEFAULT_CONFIRMATION_REGISTRATION_EMAIL_SUBJECT;
                var mailBody = mailTemplate.Replace("{FullName}", username)
                    .Replace("{Link}", confirmationLink)
                    .Replace("{rtn}", "<br/>")
                    .Replace("{TempPassword}", password);
                var sendResult = await SendApprovalNotification(username, mailSubject, mailBody);
                return sendResult != null;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ThankYouAgencyConfirmationAsync(string username)
        {
            try
            {
                var mailTemplate = _DEFAULT_CONFIRMATION_THANK_EMAIL_TEMPLATE;
                var mailSubject = _DEFAULT_CONFIRMATION_THANK_EMAIL_SUBJECT;
                var mailBody = mailTemplate.Replace("{FullName}", username)
                    .Replace("{rtn}", "<br/>");
                var sendResult = await SendApprovalNotification(username, mailSubject, mailBody);
                return sendResult != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AnnounceAdminAgencyCompletedAsync(string username)
        {
            try
            {
                var mailTemplate = _DEFAULT_AGENCY_COMPLETED_EMAIL_TEMPLATE;
                var mailSubject = _DEFAULT_AGENCY_COMPLETED_EMAIL_SUBJECT;
                var mailBody = mailTemplate.Replace("{FullName}", _appSettings.EmailDisplayName)
                    .Replace("{Agency}", username)
                    .Replace("{rtn}", "<br/>");
                var sendResult = await SendApprovalNotification(_appSettings.EmailAddress, mailSubject, mailBody);
                return sendResult != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AnnounceChangePasswordAsync(string username)
        {
            try
            {
                var mailTemplate = _DEFAULT_CHANGEPASSWORD_EMAIL_TEMPLATE;
                var mailSubject = _DEFAULT_CHANGEPASSWORD_EMAIL_SUBJECT;
                var mailBody = mailTemplate.Replace("{FullName}", _appSettings.EmailDisplayName)
                    .Replace("{rtn}", "<br/>");
                var sendResult = await SendApprovalNotification(_appSettings.EmailAddress, mailSubject, mailBody);
                return sendResult != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
