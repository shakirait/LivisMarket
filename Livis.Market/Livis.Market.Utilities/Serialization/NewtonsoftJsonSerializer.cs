using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Utilities.Serialization
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public virtual string Serialize<T>(T source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public virtual T Deserialize<T>(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return JsonConvert.DeserializeObject<T>(source);
        }

        public object Deserialize(string source, Type type)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return JsonConvert.DeserializeObject(source, type);
        }
    }
}
