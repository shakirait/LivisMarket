using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Utilities.Serialization
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T source);

        T Deserialize<T>(string source);

        object Deserialize(string source, Type type);
    }
}
