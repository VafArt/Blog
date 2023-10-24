using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.Domain.Errors;

namespace Blog.Common.Domain.Results
{
    public class ValidationResult : Result, IValidationResult
    {
        public ValidationResult(IEnumerable<Error> errors)
            : base(false, IValidationResult.ValidationError) =>
            Errors = errors;

        public IEnumerable<Error> Errors { get; }

        public static ValidationResult WithErrors(IEnumerable<Error> errors) => new(errors);
    }
}
