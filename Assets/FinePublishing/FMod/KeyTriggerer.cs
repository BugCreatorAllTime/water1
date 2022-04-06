using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyTriggerer : MonoBehaviour
{
    [SerializeField]
    KeyCode keyCode;

    [SerializeField]
    UnityEvent onKeyDown;
    [SerializeField]
    UnityEvent onKeyUp;

    // Update is called once per frame
    void Update()
    {
        ///
        if (Input.GetKeyDown(keyCode))
        {
            onKeyDown.Invoke();
            return;
        }

        ///
        if (Input.GetKeyUp(keyCode))
        {
            onKeyUp.Invoke();
            return;
        }
    }
}
