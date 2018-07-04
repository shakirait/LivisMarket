using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.Organisation.Model.Queries.Business
{
    public class OrganisationQuery : IQuery<OrganisationResponse>
    {
        public string NameOrId { get; set; }
    }
}