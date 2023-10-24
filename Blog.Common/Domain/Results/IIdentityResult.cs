using Blog.Common.Domain.Errors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Domain.Results
{
    public interface IIdentityResult
    {
        public static readonly Error IdentityError = new(
            "IdentityError",
            "An identity problem occured.");

        IEnumerable<Error> Errors { get; }
    }
}
