﻿using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Livis.Market.Data;
using System.ComponentModel.DataAnnotations;

namespace LivinMarket.Contact.Model.Commands.Business
{
    public class CustomerConfirmationCommand : ICommand
    {
        public Guid OrganisationId { get; set; }
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
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }
        public LevisUser UserConfirmation { get; set; }
    }
}