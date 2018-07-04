using Microsoft.AspNetCore.Identity;

namespace Livis.Market.Data
{
    public class LevisUser : IdentityUser
    {
        public bool IsPowerUser { get; set; }
    }
}
