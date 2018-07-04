
using LivinMarket.Contact.Model.Queries.Business;
using LivinMarket.Contact.QueryValidators.Rules;
using Livis.Market.Validation;
using System;

namespace LivinMarket.Contact.QueryValidators
{
    public class CustomerConfirmationCommandValidator : RuleBasedValidator<ContactQuery>
    {
        public CustomerConfirmationCommandValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            AddRule<ContactExistedValidationRule>();
        }
    }
}
