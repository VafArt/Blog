using Blog.CommentsService.Domain.Users;
using Dapper;
using System.Data;

namespace Blog.CommentsService.Infrastructure.TypeHandlers
{
    public class UserIdHandler : SqlMapper.TypeHandler<UserId>
    {
        public override UserId Parse(object value)
        {
            var typedValue = (Guid)value;
            return new UserId(typedValue);
        }

        public override void SetValue(IDbDataParameter parameter, UserId? value)
        {
            parameter.Value = value?.Value;
        }
    }
}
