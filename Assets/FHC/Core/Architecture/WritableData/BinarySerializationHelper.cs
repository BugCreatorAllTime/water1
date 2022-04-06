using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FH.Core.Architecture.WritableData
{
    public static class BinarySerializationHelper
    {
        public static T Deserialize<T>(byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream(bytes);

            return (T)binaryFormatter.Deserialize(memoryStream);
        }

        public static byte[] Serialize<T>(T dataObject)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, dataObject);

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Clone serializable object (clone serializable fields only)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Clone<T>(T originalObject)
        {
            return Deserialize<T>(Serialize(originalObject));
        }
    }

}