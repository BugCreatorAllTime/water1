using UnityEngine;
using UnityEditor;

namespace FH.Core.Architecture.Helper
{
    [CustomPropertyDrawer(typeof(InspectorCommandAttribute))]
    public class InspectorCommandPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (GUI.Button(position, label))
            {
                IInspectorCommandObject to = property.serializedObject.targetObject as IInspectorCommandObject;
                if (to != null)
                {
                    InspectorCommandAttribute att = attribute as InspectorCommandAttribute;
                    to.ExcuteCommand(att.IntPara, att.StringPara);
                }
            }
        }
    }

}