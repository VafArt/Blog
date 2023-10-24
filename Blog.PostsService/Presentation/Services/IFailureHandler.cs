using Blog.Common.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace Blog.PostsService.Presentation.Services
{
    public interface IFailureHandler
    {
        public IResult HandleFailure(Result result);
    }
}
