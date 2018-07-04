using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    public class CustomerContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }
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
        public string FullName { get; set; }
        [Required]
        [StringLength(64)]
        public string LastName { get; set; }
        [Required]
        [StringLength(64)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }
        public DateTime? LastOrder  { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
        [StringLength(10)]
        public string PreferredLanguage  { get; set; }
        [StringLength(10)]
        public string PreferredCurrency  { get; set; }
        public string OwnerId  { get; set; }
        [ForeignKey("OwnerId")]
        public virtual LevisUser User { get; set; }
        public Guid? PreferredShippingAddressId { get; set; }
        [ForeignKey("PreferredShippingAddressId")]
        public virtual CustomerAddress ShippingAddress { get; set; }
        public Guid? PreferredBillingAddressId { get; set; }
        [ForeignKey("PreferredBillingAddressId")]
        public virtual CustomerAddress BillingAddress { get; set; }
        public Guid? OrganisationId { get; set; }
        [ForeignKey("OrganisationId")]
        public virtual Organisation Organisation { get; set; }
        [StringLength(64)]
        public string PhoneNumber { get; set; }
    }
}
