using Blog.Common.Domain.Errors;

namespace Blog.Common.Domain.Results
{
    public class IdentityResult : Result, IIdentityResult
    {
        protected IdentityResult(bool isSuccess, IEnumerable<Error> errors)
            : base(isSuccess, IIdentityResult.IdentityError) =>
            Errors = errors;

        protected IdentityResult(bool isSuccess, Error error, IEnumerable<Error> errors)
            : base(isSuccess, error) =>
            Errors = errors;

        public IEnumerable<Error> Errors { get; }

        public static IdentityResult WithErrors(IEnumerable<Error> errors) => new(false, errors);

        public static IdentityResult<TValue> WithErrors<TValue>(TValue value, IEnumerable<Error> errors) => new(value, false, errors);

        public static IdentityResult FromAspNetIdentityResult(Microsoft.AspNetCore.Identity.IdentityResult result)
            => result.Succeeded ? Success() : WithErrors(result.Errors.Select(error => new Error(error.Code, error.Description)).ToList());

        public static IdentityResult<TValue> FromAspNetIdentityResult<TValue>(TValue value, Microsoft.AspNetCore.Identity.IdentityResult result)
            => result.Succeeded ? Success(value) : WithErrors(value, result.Errors.Select(error => new Error(error.Code, error.Description)).ToList());

        public new static IdentityResult Success() => new(true, Error.None, new List<Error>());

        public new static IdentityResult<TValue> Success<TValue>(TValue value) => new(value, true, Error.None, new List<Error>());

        protected new static IdentityResult<TValue> Create<TValue>(TValue? value) => new(value, true, new List<Error>());
    }
}
