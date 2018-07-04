using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.Product.Model.Queries.Business
{
    public class ListCategoryResponse
    {
        public CategoryResponse[] Categories { get; set; }
    }
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}