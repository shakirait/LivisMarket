﻿using System;

namespace Livis.Market.Data
{
    public class StoreProduct
    {
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
