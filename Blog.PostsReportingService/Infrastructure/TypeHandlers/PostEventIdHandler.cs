using Blog.PostsReportingService.Domain.PostEvents;
using Dapper;
using System.Data;

namespace Blog.PostsReportingService.Infrastructure.TypeHandlers
{
    public class PostEventIdHandler : SqlMapper.TypeHandler<PostEventId>
    {
        public override PostEventId Parse(object value)
        {
            var typedValue = (Guid)value;
            return new PostEventId(typedValue);
        }

        public override void SetValue(IDbDataParameter parameter, PostEventId? value)
        {
            parameter.Value = value?.Value;
        }
    }
}
