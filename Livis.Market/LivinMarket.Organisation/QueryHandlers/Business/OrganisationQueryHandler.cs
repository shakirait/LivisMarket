using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System;
using LivinMarket.Organisation.Model.Queries.Business;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LivinMarket.Organisation.QueryHandlers.Business
{
    public class OrganisationQueryHandler : QueryHandler<OrganisationQuery, OrganisationResponse>
    {
        protected override async Task<OrganisationResponse>  HandleQueryAsync(OrganisationQuery query)
        {
            OrganisationResponse response = new OrganisationResponse() {
                Organisations = new Livis.Market.Data.Organisation[] { }
            };
            if (string.IsNullOrEmpty(query.NameOrId))
            {
                response.Organisations = _context.Organisations.ToArray();
                return response;
            }
            var foundGuid = Guid.Empty;
            if(Guid.TryParse(query.NameOrId, out foundGuid))
            {
                var foundEntityById = _context.Organisations.FirstOrDefault(x => x.OrganisationId.Equals(foundGuid));
                if(foundEntityById != null)
                {
                    response.Organisations = new Livis.Market.Data.Organisation[] { foundEntityById };
                    return response;
                }
            }
            var foundEntityByName = _context.Organisations.FirstOrDefault(x => x.ShopName.Equals(query.NameOrId, StringComparison.OrdinalIgnoreCase));
            ;
            if (foundEntityByName != null)
            {
                response.Organisations = new Livis.Market.Data.Organisation[] { foundEntityByName };
                return response;
            }

            var foundEntityByEmail = _context.Organisations.FirstOrDefault(x => x.Email.Equals(query.NameOrId, StringComparison.OrdinalIgnoreCase));
            if (foundEntityByEmail != null)
            {
                response.Organisations = new Livis.Market.Data.Organisation[] { foundEntityByEmail };
                return response;
            }

            return response;
        }
    }
}
