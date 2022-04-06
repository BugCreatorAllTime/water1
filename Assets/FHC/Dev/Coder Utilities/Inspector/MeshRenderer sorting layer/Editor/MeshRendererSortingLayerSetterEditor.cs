using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System;

[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererSortingLayerSetterEditor : Editor
{
    private static MethodInfo EditorGUILayoutSortingLayerField;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(new GUIContent("Original Mesh Renderer") { }, EditorStyles.boldLabel);
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Sorting") { }, EditorStyles.boldLabel);
        SortingGui();
        Infor();
        EditorGUILayout.EndVertical();
    }

    public void OnEnable()
    {
        if (EditorGUILayoutSortingLayerField == null)
            EditorGUILayoutSortingLayerField = typeof(EditorGUILayout).GetMethod("SortingLayerField", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(GUIContent), typeof(SerializedProperty), typeof(GUIStyle) }, null);
    }

    private void SortingGui()
    {
        SerializedObject rendererSerializedObject = new SerializedObject((Renderer)target);
        SerializedProperty sortingLayerIDProperty = rendererSerializedObject.FindProperty("m_SortingLayerID");
        rendererSerializedObject.Update();

        // Sorting Layers
        {
            var renderer = (Renderer)target;
            if (renderer != null)
            {
                EditorGUI.BeginChangeCheck();

                if (EditorGUILayoutSortingLayerField != null && sortingLayerIDProperty != null)
                {
                    EditorGUILayoutSortingLayerField.Invoke(null, new object[] { new GUIContent("Sorting Layer"), sortingLayerIDProperty, EditorStyles.popup });
                }
                else
                {
                    renderer.sortingLayerID = EditorGUILayout.IntField("Sorting Layer ID", renderer.sortingLayerID);
                }

                renderer.sortingOrder = EditorGUILayout.IntField("Order in Layer", renderer.sortingOrder);

                if (EditorGUI.EndChangeCheck())
                {
                    rendererSerializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(renderer);
                }
            }
        }
    }

    private void Infor()
    {
        EditorGUILayout.HelpBox(@"Use shader with ""zwrite off"" in order to make sorting takes effect.", MessageType.Info);
    }
}

