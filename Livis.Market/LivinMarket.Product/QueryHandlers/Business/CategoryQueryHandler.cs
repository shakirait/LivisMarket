
using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System;
using LivinMarket.Product.Model.Queries.Business;

namespace LivinMarket.Product.QueryHandlers.Business
{
    public class CategoryQueryHandler : QueryHandler<CategoryQuery, ListCategoryResponse>
    {
        protected override async Task<ListCategoryResponse>  HandleQueryAsync(CategoryQuery query)
        {
            var response = new ListCategoryResponse() {
                Categories = new CategoryResponse[] { }
            };
            if (string.IsNullOrEmpty(query.NameOrId))
            {
                response.Categories =  _context.Categories.Select(x => new CategoryResponse()
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.Name
                }).ToArray();
                return response;
            }
            var foundGuid = Guid.Empty;
            if(Guid.TryParse(query.NameOrId, out foundGuid))
            {
                var foundEntityById = _context.Categories.FirstOrDefault(x => x.CategoryId.Equals(foundGuid));
                if(foundEntityById != null)
                {
                    response.Categories = new CategoryResponse[] { new CategoryResponse(){
                        CategoryId = foundEntityById.CategoryId,
                        CategoryName = foundEntityById.Name
                    }};
                    return response;
                }
            }
            var foundEntityByName = _context.Categories.FirstOrDefault(x => x.Name.Equals(query.NameOrId));
            if (foundEntityByName != null)
            {
                response.Categories = new CategoryResponse[] { new CategoryResponse(){
                        CategoryId = foundEntityByName.CategoryId,
                        CategoryName = foundEntityByName.Name
                    }};
                return response;
            }

            return response;
        }
    }
}
