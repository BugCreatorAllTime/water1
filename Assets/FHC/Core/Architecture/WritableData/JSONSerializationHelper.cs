using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FH.Core.Architecture.WritableData
{
    public static class JSONSerializationHelper
    {
        public static byte[] Serialize<T>(T dataObject)
        {
            var s = JsonUtility.ToJson(dataObject);
            return Encoding.ASCII.GetBytes(s);
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            var s = Encoding.ASCII.GetString(bytes);
            return JsonUtility.FromJson<T>(s);
        }
    }

}