using Blog.Common.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Domain.Results
{
    public class IdentityResult<TValue> : IdentityResult, IIdentityResult
    {
        private readonly TValue? _value;

        public IdentityResult(TValue? value, bool isSuccess, IEnumerable<Error> errors)
            : base(isSuccess, errors) =>
            _value = value;

        public IdentityResult(TValue? value, bool isSuccess, Error error, IEnumerable<Error> errors)
            : base(isSuccess, error, errors) =>
            _value = value;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Нельзя получить значение при ошибке!");

        public static implicit operator IdentityResult<TValue>(TValue? value) => Create(value);
    }
}
