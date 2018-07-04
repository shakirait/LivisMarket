using LivinMarket.IdentityAccess.Model.Queries.Business;
using Livis.Market.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.IdentityAccess.QueryValidators.Business
{
    public class LoginQueryValidator : RuleBasedValidator<LoginQuery>
    {
        public LoginQueryValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
