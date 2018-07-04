using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    public class Organisation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrganisationId { get; set; }
        public string ShopName { get; set; }
        public string WebsiteUrl { get; set; }
        public string OpenTime { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public string Email { get; set; }
        public string RegistrationStatus { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string BranchName { get; set; }
        public string BranchNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string Prefecture { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSendThankYouMail { get; set; }
        public bool IsSendConfirmationMail { get; set; }
        public string TokenConfirmationMail { get; set; }
        public bool IsActivateAccount { get; set; }
    }
}
