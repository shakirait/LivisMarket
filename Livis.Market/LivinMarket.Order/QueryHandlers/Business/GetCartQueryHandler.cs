using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LivinMarket.Order.Model.Queries.Business;

namespace LivinMarket.Order.QueryHandlers.Business
{
    public class GetCartQueryHandler : QueryHandler<GetCartQuery, GetCartResponse>
    {
        protected override async Task<GetCartResponse>  HandleQueryAsync(GetCartQuery query)
        {
            var cart = _context.Cart.FirstOrDefault(x => x.MarketId.Equals(query.MartketName) &&
            x.CustomerId.HasValue && x.CustomerId.Value == query.CustomerId && x.Name.Equals(query.ShopName));
            return new GetCartResponse()
            {
                Cart = cart 
            };
        }
    }
}
