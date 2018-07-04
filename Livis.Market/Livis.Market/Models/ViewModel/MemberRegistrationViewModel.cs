using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class MemberRegistrationViewModel : LayoutViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        [Display(Name = "State / Prefecture")]
        public string Prefecture { get; set; }
        [Required]
        [Display(Name = "City / Town / Village")]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-MAIL ADDRESS")]
        public string Email { get; set; }
        [EmailAddress]
        [Required]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
        [Display(Name = "For confirmation: please enter same address again")]
        public string ConfirmationEmail { get; set; }
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Min length password is 6")]
        [Display(Name = "DESIRED PASSWORD")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Min length password is 6")]
        [Display(Name = "Please enter twice for confirmation")]
        public string PasswordConfirmation { get; set; }
    }
}
