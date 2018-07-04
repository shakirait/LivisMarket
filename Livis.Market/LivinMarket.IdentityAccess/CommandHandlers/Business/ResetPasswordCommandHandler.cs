using LivinMarket.IdentityAccess.Exceptions.Business;
using LivinMarket.IdentityAccess.Model.Commands.Business;
using Livis.Common.Notifications;
using Livis.Common.Password;
using Livis.Market.Infrastructure;
using System;
using System.Threading.Tasks;

namespace LivinMarket.IdentityAccess.CommandHandlers.Business
{
    public class ResetPasswordCommandHandler : CommandHandler<ResetPasswordCommand>
    {
        private const string _UNABLE_RESET_PASSWORD = "Unable reset password.";
        private readonly INotificationService _notification;
        public ResetPasswordCommandHandler(INotificationService notification)
        {
            _notification = notification;
        }

        protected override async Task HandleCommandAsync(ResetPasswordCommand command)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(command.Email);
                var generatedPassword = await generatePassword();
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, generatedPassword);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                   var isSendMail = await _notification.ForgotPasswordAsync(user.UserName, generatedPassword);
                    if(!isSendMail)
                    {
                        throw new SendMailNotSuccessException($"Unable send mail to {command.Email} for notififing reset password.");
                    }
                }
            }catch(Exception ex)
            {
                throw new UserResetPasswordException(ex.Message, ex);
            }
        }

        private async Task<string> generatePassword()
        {
            var generatedPassword = PasswordTool.Generate(PasswordConfiguration.LengthOfPassword, PasswordConfiguration.NumberOfNonAlphanumericCharacters);
            return generatedPassword;
        }
    }
}
