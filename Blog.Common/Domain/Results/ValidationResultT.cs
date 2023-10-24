using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.Domain.Errors;

namespace Blog.Common.Domain.Results
{
    public class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        private ValidationResult(IEnumerable<Error> errors)
            : base(default, false, IValidationResult.ValidationError) =>
            Errors = errors;


        public IEnumerable<Error> Errors { get; }

        public static ValidationResult<TValue> WithErrors(IEnumerable<Error> errors) => new(errors);
    }
}
