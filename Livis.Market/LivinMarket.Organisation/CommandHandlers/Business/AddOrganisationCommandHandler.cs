using LivinMarket.Organisation.Exceptions.Business;
using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Common.Notifications;
using Livis.Common.Password;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Livis.Market.Utilities.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivinMarket.Organisation.CommandHandlers.Business
{
    public class AddOrganisationCommandHandler : CommandHandler<AddOrganisationCommand>
    {
        private readonly INotificationService _notification;
        private static Guid _privateTokenKey = new Guid("9AE0EF7C-9BCC-4AFD-B259-F67A79D4B3AA");
        private static Guid _providerUserKey = new Guid("C8037953-CF36-4F7C-9A9B-ED75A9656DFA");

        public AddOrganisationCommandHandler(INotificationService notification)
        {
            _notification = notification;
        }

        protected async override Task HandleCommandAsync(AddOrganisationCommand command)
        {
            var organisation = _mapper.Map<AddOrganisationCommand, Livis.Market.Data.Organisation>(command);
            _context.Organisations.Add(organisation);
            var username = organisation.Email;
            var password = GeneratePassword();
            var user = new LevisUser()
            {
                Email = username,
                EmailConfirmed = true,
                IsPowerUser = false,
                NormalizedEmail = username,
                NormalizedUserName = username,
                PhoneNumber = organisation.PhoneNumber,
                PhoneNumberConfirmed = true,
                UserName = username
            };
            var chkUser = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            if (chkUser.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(user, ComponentsHelper.Agency);
                var token = GenerateToken(user);
                command.ConfirmationLink = command.ConfirmationLink + "?token=" + token;
                await _notification.AgencyConfirmationAsync(command.ConfirmationLink, username, password);
                organisation.IsActivateAccount = false;
                organisation.IsSendConfirmationMail = true;
                organisation.IsSendThankYouMail = false;
                organisation.TokenConfirmationMail = token;
                _context.SaveChanges();
                command.OrganisationId = organisation.OrganisationId;
            }
            else
            {
                throw new UnableCreateUserException("Please contact Administrator. System is unable creating organisation.");
            }
        }

        private string GeneratePassword()
        {
            var generatedPassword = PasswordTool.Generate(PasswordConfiguration.LengthOfPassword, PasswordConfiguration.NumberOfNonAlphanumericCharacters);
            return generatedPassword;
        }

        private string GenerateToken(LevisUser user)
        {
            var parts = new List<byte[]>
            {
                BitConverter.GetBytes(DateTime.UtcNow.ToBinary()),
                (_providerUserKey).ToByteArray(),
                _privateTokenKey.ToByteArray(),
                Encoding.UTF8.GetBytes(user.UserName)
            };

            var tokenBytes = new byte[parts.Sum(s => s.Length)];
            var offset = 0;
            foreach (var part in parts)
            {
                Buffer.BlockCopy(part, 0, tokenBytes, offset, part.Length);
                offset += part.Length;
            }
            return Base64UrlTextEncoder.Encode(tokenBytes.ToArray());
        }
    }
}
