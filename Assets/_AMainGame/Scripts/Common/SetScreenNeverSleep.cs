using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenNeverSleep : MonoBehaviour
{
    public void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
