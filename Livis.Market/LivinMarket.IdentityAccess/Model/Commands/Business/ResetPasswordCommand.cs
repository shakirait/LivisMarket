using Livis.Market.Caching;
using Livis.Market.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LivinMarket.IdentityAccess.Model.Commands.Business
{
    public class ResetPasswordCommand : ICommand
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
