using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rules.Domain;

namespace Rules.Authoring
{
    public class ConditionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer,value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            if (jObject["Property"] != null &&  jObject["Value"] != null && jObject["Op"]!=null)
            {
                return jObject.ToObject<Condition>(serializer);
            }
            if (jObject["ConditionOperator"]!= null)
            {
                return jObject.ToObject<AggregateCondition>(serializer);

            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AggregateCondition) || objectType == typeof(Condition);
        }
    }
}