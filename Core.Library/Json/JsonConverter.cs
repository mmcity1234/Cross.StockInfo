using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Json
{
    public class JsonConverter
    {
        public static String SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T DeserializeObject<T>(Stream stream)
        {
            T resultObj = default(T);
            using (StreamReader reader = new StreamReader(stream))
            {
                using (JsonReader jsonReader = new JsonTextReader(reader))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    // read the json from a stream
                    // json size doesn't matter because only a small piece is read at a time from the HTTP request
                    T p = serializer.Deserialize<T>(jsonReader);
                }
            }
            return resultObj;
        }
    }
}
