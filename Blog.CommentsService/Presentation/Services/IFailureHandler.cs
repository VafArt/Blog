using Blog.Common.Domain.Results;

namespace Blog.CommentsService.Presentation.Services
{
    public interface IFailureHandler
    {
        public IResult HandleFailure(Result result);
    }
}
