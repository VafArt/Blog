using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Domain.Errors
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "Ошибки нет");

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }

        public static implicit operator string(Error error) => error.Code;

        public static bool operator ==(Error? a, Error? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Code == b.Code;
        }

        public static bool operator !=(Error? a, Error? b)
        {
            return !(a == b);
        }

        public bool Equals(Error? other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            var error = obj as Error;
            if (error == null) return false;
            return this == error;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
