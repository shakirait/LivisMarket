using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Livis.Market.Data
{
    public class SerializableCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public Guid? CustomerId { get; set; }
        public string Name { get; set; }
        public string MarketId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Data { get; set; }
    }
}
