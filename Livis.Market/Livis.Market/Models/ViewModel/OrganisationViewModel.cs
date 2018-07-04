using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class OrganisationView : OrganisationViewModel
    {
        public string ViewUrl { get; set; }
        public string EditUrl { get; set; }
    }
    public class OrganisationViewModel
    {
        public Guid OrganisationId { get; set; }
        [Display(Name = "Account Holder")]
        public string AccountHolder { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Display(Name = "Bank Code")]
        public string BankNumber { get; set; }
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [Display(Name = "Branch Code")]
        public string BranchNumber { get; set; }
        [Required]
        [Display(Name = "City / Town / Village")]
        public string City { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Fax No")]
        public string FaxNumber { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Open Time")]
        public string OpenTime { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        [Display(Name = "State / Prefecture")]
        public string Prefecture { get; set; }
        [Required]
        [Display(Name = "Registration Status")]
        public string RegistrationStatus { get; set; }
        [Display(Name = "Name of Agency")]
        public string ShopName { get; set; }
        [Required]
        public string Street { get; set; }
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }
    }
}
