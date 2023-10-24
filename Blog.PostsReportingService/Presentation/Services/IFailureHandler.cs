using Blog.Common.Domain.Results;

namespace Blog.PostsReportingService.Presentation.Services
{
    public interface IFailureHandler
    {
        public IResult HandleFailure(Result result);
    }
}
