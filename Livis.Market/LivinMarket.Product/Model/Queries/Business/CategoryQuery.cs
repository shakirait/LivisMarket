using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.Product.Model.Queries.Business
{
    public class CategoryQuery : IQuery<ListCategoryResponse>
    {
        public string NameOrId { get; set; }
    }
}