using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FH.Core.Architecture.WritableData
{
    public class WritableDataDeleteAll
    {
        [MenuItem("Delete All WritableData", menuItem = "FH/PlayerData/Delete All WritableData")]
        public static void DeleteAll()
        {
            Debug.Log(string.Format("Deleted all files from {0}", Application.persistentDataPath));
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
            
        }
    }

}