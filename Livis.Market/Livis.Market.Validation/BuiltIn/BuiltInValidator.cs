using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Validation.BuiltIn
{
    public class BuiltInValidator<TRequest> : RuleBasedValidator<TRequest>
    {
        public BuiltInValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<RequestMustBeNotNullRule<TRequest>>();
            AddRule<DataAnnotationValidationMustPassRule<TRequest>>();
        }
    }
}
