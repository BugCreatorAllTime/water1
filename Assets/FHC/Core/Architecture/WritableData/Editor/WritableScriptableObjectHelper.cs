using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FH.Core.Architecture.WritableData
{
    [InitializeOnLoad]
    public class WritableScriptableObjectHelper
    {
        static WritableScriptableObjectHelper()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        static void OnSelectionChanged()
        {
            var targets = Selection.objects;
            for (int i = 0; i < targets.Length; i++)
            {
                var rightTarget = targets[i] as IWritableScriptableObjectHelper;
                if (rightTarget != null)
                {
                    (rightTarget as IWritableScriptableObjectHelper).Editor_LoadCurrentData();
                }
            }
        }
    }

}