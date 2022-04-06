using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogTypesCountDisplayer : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void OnEnable()
    {
        ///
        UpdateCount();

        ///
        Reporter.OnLogAdded += Reporter_OnLogAdded;
    }

    public void OnDisable()
    {
        ///
        Reporter.OnLogAdded -= Reporter_OnLogAdded;
    }

    private void Reporter_OnLogAdded()
    {
        UpdateCount();
    }

    void UpdateCount()
    {
        string format = "{0} - {1} - {2} - {3} - {4}";

        text.text = string.Format(format,
            Reporter.GetLogTypeCount(LogType.Exception)
            , Reporter.GetLogTypeCount(LogType.Error)
            , Reporter.GetLogTypeCount(LogType.Assert)
            , Reporter.GetLogTypeCount(LogType.Warning)
            , Reporter.GetLogTypeCount(LogType.Log)
            );
    }
}
