using Dapper;
using System.Data;

namespace Blog.Common.Infrastructure.TypeHandlers
{
    public class StringListHandler : SqlMapper.TypeHandler<List<string>>
    {
        public override List<string> Parse(object value)
        {
            string[] typedValue = (string[])value;
            return typedValue?.ToList() ?? new List<string>();
        }

        public override void SetValue(IDbDataParameter parameter, List<string> value)
        {
            parameter.Value = value;
        }
    }
}
