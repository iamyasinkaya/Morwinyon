namespace Morwinyon.Caching
{

    /// <summary>
    /// Provides extension methods for serialization and deserialization of objects.
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serializes the specified object to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialized.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>A byte array representing the serialized object.</returns>
        public static byte[] Serialize<T>(this T obj)
        {
            // Implement your own serialization logic here
            // For simplicity, using JSON serialization in this example
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return System.Text.Encoding.UTF8.GetBytes(jsonString);
        }

        /// <summary>
        /// Deserializes the specified byte array to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to be deserialized.</typeparam>
        /// <param name="data">The byte array to be deserialized.</param>
        /// <returns>An object of the specified type deserialized from the byte array.</returns>
        public static T Deserialize<T>(this byte[] data)
        {
            // Implement your own deserialization logic here
            // For simplicity, using JSON deserialization in this example
            var jsonString = System.Text.Encoding.UTF8.GetString(data);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}