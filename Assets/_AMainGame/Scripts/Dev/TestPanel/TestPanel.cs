using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestPanel : MonoBehaviour
{
    [SerializeField]
    private float activateHoldTime = 5.0f;
    [SerializeField]
    private GameObject panel;

    [Space]
    [SerializeField]
    private UnityEvent onActivate;

    public void OnHoldBegin()
    {
        StartCoroutine(WaitForHold());
    }

    private IEnumerator WaitForHold()
    {
        ///
        float t = 0;

        ///
        while (Input.GetMouseButton(0))
        {
            ///
            t += Time.unscaledDeltaTime;

            ///
            if (t >= activateHoldTime)
            {
                break;
            }

            ///
            yield return null;
        }

        ///
        if (t >= activateHoldTime)
        {
            ///
            onActivate?.Invoke();

            ///
            panel.SetActive(true);
        }
    }

    public void ShowLog()
    {
        ///
        ReporterUISwitcher.LockedUI = false;
        Reporter.Instance.doShow();
    }
}
