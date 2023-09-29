using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Common.Domain
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error) => _value = value;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Нельзя получить значение при ошибке!");

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
