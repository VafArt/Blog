using Blog.PostsService.Domain.Posts;
using Dapper;
using System.Data;

namespace Blog.PostsService.Infrastructure.TypeHandlers
{
    public class PostIdHandler : SqlMapper.TypeHandler<PostId>
    {
        public override PostId Parse(object value)
        {
            var typedValue = (Guid)value;
            return new PostId(typedValue);
        }

        public override void SetValue(IDbDataParameter parameter, PostId value)
        {
            parameter.Value = value.Value;
        }
    }
}
