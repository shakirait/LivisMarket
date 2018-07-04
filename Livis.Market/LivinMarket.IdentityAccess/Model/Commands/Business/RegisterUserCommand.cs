using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LivinMarket.IdentityAccess.Model.Commands.Business
{
    public class RegisterUserCommand : ICommand
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
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
        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
