using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    public class Party
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PartyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string AddressName { get; set; }
        [StringLength(64)]
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public bool IsPrimary { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string ZipPostalCode { get; set; }
    }
}
