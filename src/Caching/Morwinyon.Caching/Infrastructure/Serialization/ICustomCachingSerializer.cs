using System;

namespace Morwinyon.Caching
{
    /// <summary>
    /// Interface for a versatile caching data serializer.
    /// </summary>
    public interface ICustomCachingSerializer
    {
        /// <summary>
        /// Gets the serializer name.
        /// </summary>
        /// <value>The serializer name.</value>
        string SerializerName { get; }

        /// <summary>
        /// Serialize the specified value.
        /// </summary>
        /// <returns>The serialized data as bytes.</returns>
        /// <param name="value">The value to be serialized.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        byte[] SerializeData<T>(T value);

        /// <summary>
        /// Deserialize the specified bytes.
        /// </summary>
        /// <returns>The deserialized value.</returns>
        /// <param name="bytes">The byte array to be deserialized.</param>
        /// <typeparam name="T">The type of the value to be deserialized.</typeparam>
        T DeserializeData<T>(byte[] bytes);

        /// <summary>
        /// Deserialize the specified bytes.
        /// </summary>
        /// <returns>The deserialized object.</returns>
        /// <param name="bytes">The byte array to be deserialized.</param>
        /// <param name="type">The type of the object to be deserialized.</param>
        object DeserializeObject(byte[] bytes, Type type);

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <returns>The serialized object as an array of bytes.</returns>
        /// <param name="obj">The object to be serialized.</param>
        ArraySegment<byte> SerializeObjectData(object obj);

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <returns>The deserialized object.</returns>
        /// <param name="value">The byte array representing the serialized object.</param>
        object DeserializeObjectData(ArraySegment<byte> value);
    }

}
