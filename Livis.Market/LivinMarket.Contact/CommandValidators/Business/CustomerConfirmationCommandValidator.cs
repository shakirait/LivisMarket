
using LivinMarket.Contact.CommandValidators.Business.Rules;
using LivinMarket.Contact.Model.Commands.Business;
using Livis.Market.Validation;
using System;

namespace LivinMarket.Contact.CommandValidators.Business
{
    public class CustomerConfirmationCommandValidator : RuleBasedValidator<CustomerConfirmationCommand>
    {
        public CustomerConfirmationCommandValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            AddRule<NotFoundCustomerValidationRule>();
        }
    }
}
