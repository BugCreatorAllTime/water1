using UnityEngine;
using System.Collections;
using System.IO;

namespace FH.Core.Architecture.WritableData
{
    public static class BinaryFileHelper
    {
        public static void SaveFile(byte[] bytes, string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Flush();
            fileStream.Close();
        }

        public static byte[] LoadFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            return bytes;
        }
    }

}