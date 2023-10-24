using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.Domain.Errors;

namespace Blog.Common.Domain.Results
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "A validation problem occured.");

        IEnumerable<Error> Errors { get; }
    }
}
