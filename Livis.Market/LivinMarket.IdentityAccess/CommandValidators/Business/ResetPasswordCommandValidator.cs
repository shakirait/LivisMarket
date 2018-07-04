using LivinMarket.IdentityAccess.CommandValidators.Business.Rules;
using LivinMarket.IdentityAccess.Model.Commands.Business;
using Livis.Market.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.IdentityAccess.CommandValidators.Business
{
    public class ResetPasswordCommandValidator : RuleBasedValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            AddRule<UserNotFoundValidationRule>();
        }
    }
}
