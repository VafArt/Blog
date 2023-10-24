using Blog.CommentsService.Domain.Comments;
using Dapper;
using System.Data;

namespace Blog.CommentsService.Infrastructure.TypeHandlers
{
    public class CommentIdHandler : SqlMapper.TypeHandler<CommentId>
    {
        public override CommentId Parse(object value)
        {
            var typedValue = (Guid)value;
            return new CommentId(typedValue);
        }

        public override void SetValue(IDbDataParameter parameter, CommentId? value)
        {
            parameter.Value = value?.Value;
        }
    }
}
