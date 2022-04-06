using System.IO;
using UnityEngine;

namespace FH.Core.Architecture.WritableData
{
    public static class PersistentFileHelper
    {
        public static void SaveFile(byte[] bytes, string fileName)
        {
            BinaryFileHelper.SaveFile(bytes, GetPath(fileName));
        }

        public static byte[] LoadFile(string fileName)
        {
            return BinaryFileHelper.LoadFile(GetPath(fileName));
        }

        public static bool FileExist(string fileName)
        {
            return File.Exists(GetPath(fileName));
        }

        public static void DeleteFile(string fileName)
        {
            System.IO.File.Delete(GetPath(fileName));
        }

        static string GetPath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }

}