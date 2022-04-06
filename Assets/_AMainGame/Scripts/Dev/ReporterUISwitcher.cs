using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReporterUISwitcher : MonoBehaviour
{
#if UNITY_STANDALONE
    const string Password = "yougotit";
#else
    const string Password = "giang";
#endif

    [SerializeField]
    InputField inputField;

    [SerializeField]
    UnityEvent onUnlock = new UnityEvent();

    public static bool LockedUI
    {
        get
        {
            return Reporter.LockedUI;
        }
        set
        {
            Reporter.LockedUI = value;
        }
    }

    public void TryUnlock()
    {
        if (inputField.text == Password)
        {
            ///
            LockedUI = false;
            Reporter.Instance.doShow();

            ///
            onUnlock.Invoke();
        }
    }
}
