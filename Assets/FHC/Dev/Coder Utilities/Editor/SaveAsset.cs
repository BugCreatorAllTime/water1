using UnityEditor;
using System.Collections;

namespace FH.DevTool
{
    public class SaveAsset
    {
        [MenuItem("FH/Save all assets", priority = 9)]
        public static void SaveAllAssets()
        {
            AssetDatabase.SaveAssets();
        }
    }

}