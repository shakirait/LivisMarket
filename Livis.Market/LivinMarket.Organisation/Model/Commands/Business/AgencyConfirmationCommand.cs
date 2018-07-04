using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Livis.Market.Data;
using System.ComponentModel.DataAnnotations;

namespace LivinMarket.Organisation.Model.Commands.Business
{
    public class AgencyConfirmationCommand : ICommand
    {
        public Guid OrganisationId { get; set; }
        public string ShopName { get; set; }
        public string WebsiteUrl { get; set; }
        public string OpenTime { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string BranchName { get; set; }
        public string BranchNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        [Required(ErrorMessage = "The prefecture is required")]
        public string Prefecture { get; set; }
        [Required(ErrorMessage = "The city is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "The street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "The postcode is required")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "The phone is required")]
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }
        public LevisUser UserConfirmation { get; set; }
    }
}
