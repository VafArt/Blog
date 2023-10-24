
using Microsoft.AspNetCore.Mvc;

namespace Blog.Common.ProblemDetailsImplementation
{
    public class NotFoundProblemDetails : ProblemDetails
    {
        public Guid Id { get; set; }
    }
}
