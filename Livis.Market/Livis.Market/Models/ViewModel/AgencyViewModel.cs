using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class AgencyViewModel : LayoutViewModel
    {
        public Guid OrganisationId { get; set; }
        [Display(Name = "NAME OF AGENCY")]
        public string ShopName { get; set; }
        [Required]
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
        [Display(Name = "Fax No")]
        public string FaxNumber { get; set; }
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Min length password is 6")]
        [Display(Name = "DESIRED PASSWORD")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Min length password is 6")]
        [Display(Name = "Please enter twice for confirmation")]
        public string PasswordConfirmation { get; set; }
        [Display(Name = "FINANCIAL INSTITUTION NAME")]
        public string BankName { get; set; }
        [Display(Name = "Bank Code")]
        public string BankNumber { get; set; }
        [Display(Name = "BRANCH NAME")]
        public string BranchName { get; set; }
        [Display(Name = "Branch Code")]
        public string BranchNumber { get; set; }
        [Display(Name = "ACCOUNT HOLDER")]
        public string AccountHolder { get; set; }
        [Display(Name = "ACCOUNT NUMBER")]
        public string AccountNumber { get; set; }
        [Display(Name = "Open Time")]
        public string OpenTime { get; set; }
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
    }
}
