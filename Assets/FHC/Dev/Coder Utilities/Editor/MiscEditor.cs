using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MiscEditor
{

    [MenuItem("FH/Count Selected Objects")]
    public static void SaveAllAssets()
    {
        Debug.LogFormat("Selected {0} objects", Selection.objects.Length);
    }
}
