using Blog.Common.Domain.Results;

namespace Blog.IdentityService.Presentation.Services
{
    public interface IFailureHandler
    {
        public IResult HandleFailure(Result result);
    }
}
