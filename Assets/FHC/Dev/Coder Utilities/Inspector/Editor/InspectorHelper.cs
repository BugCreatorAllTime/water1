using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FH.DevTool
{
    public static class InspectorHelper
    {
        #region Button colors
        public static Color ButtonColorSafe
        {
            get
            {
                return Color.green;
            }
        }

        public static Color ButtonColorDangerous
        {
            get
            {
                return Color.red;
            }
        }

        public static Color ButtonColorBeNoticed
        {
            get
            {
                return Color.yellow;
            }
        }
        #endregion

        public static void DrawSerializedObject(SerializedObject so, bool applyChange = false)
        {
            const string initialPropertyName = "FHInitialProperty";

            EditorGUI.BeginChangeCheck();

            SerializedProperty property = null;

            property = so.FindProperty(initialPropertyName);
            if (property == null)
            {
                EditorGUILayout.HelpBox(string.Format("InspectorHelper.DrawSerializedObject\n{0} property not found!\nObject name: {1}", initialPropertyName, so.targetObject.name), MessageType.Error, true);
                return;
            }

            property.Next(true);
            EditorGUILayout.PropertyField(property, true);
            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }

            if (applyChange && EditorGUI.EndChangeCheck())
            {
                so.ApplyModifiedProperties();
                foreach (var item in so.targetObjects)
                {
                    EditorUtility.SetDirty(item);
                }
            }
        }
        public static bool DrawButton(string label, Color color)
        {
            bool clicked = false;

            var savedColor = GUI.color;
            GUI.color = color;

            clicked = GUILayout.Button(label);

            GUI.color = savedColor;

            return clicked;
        }

        public static bool DrawButton(string label, Color color, float width, float height)
        {
            bool clicked = false;

            var savedColor = GUI.color;
            GUI.color = color;

            clicked = GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height));

            GUI.color = savedColor;

            return clicked;
        }

        public static void DrawFixedWidthLabelPropertyField(Rect totalPosition, SerializedProperty property, float labelWidth, string label = null)
        {
            float savedWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;

            if (label == null)
            {
                label = property.name;
            }

            EditorGUI.BeginProperty(totalPosition, new GUIContent(label), property);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = EditorGUI.IntField(totalPosition, label, property.intValue);
                    break;
                case SerializedPropertyType.Boolean:
                    property.boolValue = EditorGUI.Toggle(totalPosition, label, property.boolValue);
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = EditorGUI.FloatField(totalPosition, label, property.floatValue);
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = EditorGUI.TextField(totalPosition, label, property.stringValue);
                    break;
                default:
                    throw new System.NotSupportedException();
            }

            EditorGUI.EndProperty();

            EditorGUIUtility.labelWidth = savedWidth;
        }

        public static void PingObject(string folderPath)
        {
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(folderPath);
        }

    }

}