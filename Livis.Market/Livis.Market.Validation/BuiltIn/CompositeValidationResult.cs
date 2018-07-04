using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Livis.Market.Validation.BuiltIn
{
    public class CompositeValidationResult : ValidationResult
    {
        public CompositeValidationResult(IList<ValidationResult> results)
            : base(results[0])
        {
            Results = results;
        }

        public IList<ValidationResult> Results { get; }
    }
}
