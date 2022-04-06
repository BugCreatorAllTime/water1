using UnityEngine;
using UnityEditor;

namespace FH.Core.Architecture
{
    [CustomPropertyDrawer(typeof(OrderedEventDispatcher))]
    public class OrderedEventDispatcherInspector : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty listenersProperty = property.FindPropertyRelative("listeners");

            int unityEventsCount = listenersProperty.arraySize;

            string prefix = "";
            if (unityEventsCount > 0)
            {
                prefix = string.Format("({0}) ", unityEventsCount);
            }

            EditorGUI.PropertyField(position, property, new GUIContent(label) { text = prefix + label.text }, true);

        }
    }

}