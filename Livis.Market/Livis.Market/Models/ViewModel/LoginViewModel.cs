using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class LoginViewModel : LayoutViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string RegistrationUrl { get; set; }

        public string ResetPasswordUrl { get; set; }
    }
}
