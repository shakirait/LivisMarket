using Livis.Market.Caching;
using Livis.Market.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LivinMarket.IdentityAccess.Model.Queries.Business
{
    public class LoginQuery : IQuery<LoginResponse>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
