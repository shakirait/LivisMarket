using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class FindUsernameView
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "DESIRED PASSWORD")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Please enter twice for confirmation")]
        public string PasswordConfirmation { get; set; }
        [Display(Name = "Is Lock")]
        public bool IsLock { get; set; }
    }
}
