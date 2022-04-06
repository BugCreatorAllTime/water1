using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.Architecture.WritableData
{
    public static class SerializationHelper
    {
        public static byte[] Serialize<T>(T dataObject, FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.Binary:
                    return BinarySerializationHelper.Serialize(dataObject);
                case FileFormat.JSON:
                    return JSONSerializationHelper.Serialize(dataObject);
                default:
                    throw new System.NotImplementedException();
            }
        }

        public static T Deserialize<T>(byte[] bytes, FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.Binary:
                    return BinarySerializationHelper.Deserialize<T>(bytes);
                case FileFormat.JSON:
                    return JSONSerializationHelper.Deserialize<T>(bytes);
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}