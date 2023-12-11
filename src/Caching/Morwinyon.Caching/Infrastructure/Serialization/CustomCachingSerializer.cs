using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Morwinyon.Caching
{
    /// <summary>
    /// Custom caching serializer implementation using JSON serialization.
    /// </summary>
    public class CustomCachingSerializer : ICustomCachingSerializer
    {
        private readonly JsonSerializer jsonSerializer;
        private readonly string name;

        private static readonly UTF8Encoding utf8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        /// <summary>
        /// Gets the name of the serializer.
        /// </summary>
        public string SerializerName => name;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomCachingSerializer"/> class.
        /// </summary>
        /// <param name="name">The name of the serializer.</param>
        /// <param name="serializerSettings">JSON serializer settings.</param>
        public CustomCachingSerializer(string name, JsonSerializerSettings serializerSettings)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            jsonSerializer = JsonSerializer.Create(serializerSettings);
        }

        /// <summary>
        /// Deserializes the byte array to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="bytes">The byte array to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public T DeserializeData<T>(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            using (var streamReader = new StreamReader(memoryStream, utf8Encoding))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return jsonSerializer.Deserialize<T>(jsonTextReader);
            }
        }

        /// <summary>
        /// Deserializes the byte array to the specified type.
        /// </summary>
        /// <param name="bytes">The byte array to deserialize.</param>
        /// <param name="type">The type of the object to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public object DeserializeObject(byte[] bytes, Type type)
        {
            using (var memoryStream = new MemoryStream(bytes))
            using (var streamReader = new StreamReader(memoryStream, utf8Encoding))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return jsonSerializer.Deserialize(jsonTextReader, type);
            }
        }

        /// <summary>
        /// Deserializes the byte array segment to an object with type information.
        /// </summary>
        /// <param name="value">The byte array segment to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public object DeserializeObjectData(ArraySegment<byte> value)
        {
            using (var memoryStream = new MemoryStream(value.Array, value.Offset, value.Count, writable: false))
            using (var textWriter = new StreamReader(memoryStream))
            using (var jsonTextReader = new JsonTextReader(textWriter))
            {
                jsonTextReader.Read();
                if (jsonTextReader.TokenType == JsonToken.StartArray)
                {
                    // read type
                    var typeName = jsonTextReader.ReadAsString();
                    var type = Type.GetType(typeName, throwOnError: true);// Get type

                    // read object
                    jsonTextReader.Read();
                    return jsonSerializer.Deserialize(jsonTextReader, type);
                }
                else
                {
                    throw new InvalidDataException("JsonTranscoder only supports [\"TypeName\", object]");
                }
            }
        }

        /// <summary>
        /// Serializes the object to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="value">The object to serialize.</param>
        /// <returns>The serialized byte array.</returns>
        public byte[] SerializeData<T>(T value)
        {
            using (var memoryStream = new MemoryStream())
            using (var textWriter = new StreamWriter(memoryStream, utf8Encoding))
            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                jsonSerializer.Serialize(jsonWriter, value);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Serializes the object to a byte array segment with type information.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized byte array segment.</returns>
        public ArraySegment<byte> SerializeObjectData(object obj)
        {
            var typeName = TypeHelper.BuildTypeName(obj.GetType()); // Get type 

            using (var memoryStream = new MemoryStream())
            using (var textWriter = new StreamWriter(memoryStream))
            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                jsonWriter.WriteStartArray();
                jsonWriter.WriteValue(typeName);
                jsonSerializer.Serialize(jsonWriter, obj);

                jsonWriter.WriteEndArray();
                jsonWriter.Flush();

                return new ArraySegment<byte>(memoryStream.ToArray(), 0, (int)memoryStream.Length);
            }
        }
    }
}
