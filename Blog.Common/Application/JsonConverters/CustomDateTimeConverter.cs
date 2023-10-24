using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Common.Application.JsonConverters
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format;
        public CustomDateTimeConverter(string format)
        {
            _format = format;
        }
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
            => writer.WriteStringValue(date.ToString(_format));

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString() ?? default(DateTime).ToString(), _format, null);
        }
    }
}
