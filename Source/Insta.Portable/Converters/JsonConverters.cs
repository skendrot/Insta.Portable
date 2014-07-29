using System;
using Insta.Portable.Extensions;
using Newtonsoft.Json;

namespace Insta.Portable.Converters
{
    public class BoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value != null && (reader.Value.ToString() == "1");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }

    public class EpochDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime?) value;
            var epoch = date.HasValue ? date.Value.ToEpoch() : 0;
            writer.WriteValue(epoch);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var timestamp = float.Parse(reader.Value.ToString());
            if (timestamp == 0) return null;
            DateTime? date = timestamp.FromEpoch();
            return date;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (DateTime?);
        }
    }
}
