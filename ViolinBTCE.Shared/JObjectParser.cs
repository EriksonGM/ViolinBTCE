using System;
using Newtonsoft.Json.Linq;

namespace ViolinBtce.Shared
{
    public class JObjectParser
    {
        public static Object ReadFromJObject(JObject jObject, Type dtoType)
        {
            var dto = jObject.ToObject(dtoType);
            return dto;
        }
    }
}
