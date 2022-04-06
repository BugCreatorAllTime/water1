using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FH.DevTool
{
    public class ConfirmationWindow : EditorWindow
    {
        string confirmCode = "";
        string content = "";
        string enteredCoded = "";
        System.Action confirmedCallback;

        public static void ShowWindow(string content, string confirmCode, System.Action confirmedCallback)
        {
            var window = GetWindow<ConfirmationWindow>(true, "Confirmation");
            window.confirmCode = confirmCode;
            window.content = content;
            window.enteredCoded = "";
            window.confirmedCallback = confirmedCallback;
            window.Show();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.HelpBox(content, MessageType.Info);
            EditorGUILayout.LabelField(string.Format("Type \"{0}\" into box below to confirm.", confirmCode));
            enteredCoded = EditorGUILayout.TextField(enteredCoded);

            if (InspectorHelper.DrawButton("Confirm", InspectorHelper.ButtonColorBeNoticed))
            {
                if (enteredCoded == confirmCode)
                {
                    Close();
                    if (confirmedCallback != null)
                    {
                        confirmedCallback();
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "Entered code is invalid", "OK");
                }
            }

            EditorGUILayout.EndVertical();
        }
    }

}