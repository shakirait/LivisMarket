using LivinMarket.Organisation.CommandValidators.Business.Rules;
using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Market.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.Organisation.CommandValidators.Business
{
    public class AddOrganisationCommandValidator : RuleBasedValidator<AddOrganisationCommand>
    {
        public AddOrganisationCommandValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            AddRule<UserExistedValidationRule>();
        }
    }
}
