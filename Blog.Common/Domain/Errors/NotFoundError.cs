

namespace Blog.Common.Domain.Errors
{
    public class NotFoundError : Error
    {
        public string Parameter { get; set; }

        public NotFoundError(string parameter, string code = "NotFound", string message = "There is no entity with the specified parameter") : base(code, message)
        {
            Parameter = parameter;
        }
    }
}
