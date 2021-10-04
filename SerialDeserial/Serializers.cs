using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerialDeserial
{
    public static class Serializers
    {
        /// <summary>
        /// Json Serialization and Deserialization class
        /// </summary>
        /// <typeparam name="T">Type of object to be saved or returned</typeparam>
        public static class JsonSerializerClass<T> where T : class
        {
            /// <summary>
            /// Json Serialization with saving to file
            /// </summary>
            /// <param name="data">Object to be serialized</param>
            /// <param name="filePath">Where to save serialized object</param>
            public static void DoJsonSerialization(object data, string filePath)
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Formatting = Formatting.Indented;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var writer = new StreamWriter(filePath))
                {
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        jsonSerializer.Serialize(jsonWriter, data);
                    }
                }
            }

            /// <summary>
            /// Json Deserialization
            /// </summary>
            /// <param name="dataType">Type of object to be returned</param>
            /// <param name="filePath">Where file is</param>
            /// <returns>Object deserialized as T where T : class</returns>
            public static T DoJsonDeserialization(Type dataType, string filePath)
            {
                JObject jObject = null;
                var jsonSerializer = new JsonSerializer();

                if (File.Exists(filePath))
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            jObject = jsonSerializer.Deserialize(jsonReader) as JObject;
                        }
                    }
                }

                var obj = jObject.ToObject(dataType);
                return obj as T;
            }
        }

        /// <summary>
        /// Xml Serialization and Deserialization class
        /// </summary>
        /// <typeparam name="T">Type of object to be saved or returned</typeparam>
        public static class XmlSerializerClass<T> where T : class
        {
            /// <summary>
            /// Xml Serialization with saving to file
            /// </summary>
            /// <param name="dataType">Type of object to be saved</param>
            /// <param name="data">Object to be serialized</param>
            /// <param name="filePath">Where to save serialized object</param>
            public static void DoXmlSerialization(Type dataType, T data, string filePath)
            {
                var xmlSerializer = new XmlSerializer(dataType);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var writer = new StreamWriter(filePath))
                {
                    xmlSerializer.Serialize(writer, data);
                }
            }

            /// <summary>
            /// Xml Deserialization
            /// </summary>
            /// <param name="dataType">Type of object to be returned</param>
            /// <param name="filePath">Where file is</param>
            /// <returns>Object deserialized as T where T : class</returns>
            public static T DoXmlDeserialization(Type dataType, string filePath)
            {
                T obj = null;

                var xmlSerializer = new XmlSerializer(dataType);

                if (File.Exists(filePath))
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        obj = (T)xmlSerializer.Deserialize(reader);
                    }
                }

                return obj;
            }
        }
    }
}
