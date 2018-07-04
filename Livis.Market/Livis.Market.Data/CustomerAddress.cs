using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    [Flags]
    public enum CustomerAddressTypeEnum
    {
        Shipping = 2,
        Billing = 4
    }

    public class CustomerAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddressId { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Modified { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string ModifierId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(64)]
        public string LastName { get; set; }
        [StringLength(64)]
        public string FirstName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(64)]
        public string StreetAndHouseNumber { get; set; }
        [StringLength(50)]
        public string CityOrTownOrVillage  { get; set; }
        [StringLength(64)]
        public string PostCode { get; set; }
        [StringLength(64)]
        public string Prefecture { get; set; }
        [StringLength(64)]
        public string PhoneNumber { get; set; }
        [Required]
        public CustomerAddressTypeEnum AddressType { get; set; }
    }
}
