using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.HelperComponent
{
    public class RectTransformSnap : MonoBehaviour
    {
        [SerializeField]
        List<RectTransform> rectTransforms = new List<RectTransform>();
        [SerializeField]
        List<Vector2> anchoredPositions = new List<Vector2>();
        [SerializeField]
        List<Vector2> anchoredPositionsEditor = new List<Vector2>();

        public void Snap()
        {
            Snap(anchoredPositions);
        }

        void Snap(List<Vector2> anchoredPositions)
        {
            for (int i = 0; i < rectTransforms.Count; i++)
            {
                rectTransforms[i].anchoredPosition = anchoredPositions[i];
            }
        }

        [ContextMenu("SaveCurrentAnchoredPositions")]
        void SaveCurrentAnchoredPositions()
        {
            anchoredPositions = new List<Vector2>();

            for (int i = 0; i < rectTransforms.Count; i++)
            {
                anchoredPositions.Add(rectTransforms[i].anchoredPosition);
            }

#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "SaveCurrentAnchoredPositions");
            UnityEditor.Undo.FlushUndoRecordObjects();
#endif
        }

        void SaveCurrentAnchoredPositionsEditor()
        {
            anchoredPositionsEditor = new List<Vector2>();

            for (int i = 0; i < rectTransforms.Count; i++)
            {
                anchoredPositionsEditor.Add(rectTransforms[i].anchoredPosition);
            }
        }

        [ContextMenu("TestSnap")]
        void TestSnap()
        {
            SaveCurrentAnchoredPositionsEditor();
            Snap(anchoredPositions);
        }

        [ContextMenu("TestRestoreSnap")]
        void TestRestoreSnap()
        {
            Snap(anchoredPositionsEditor);
        }
    }

}