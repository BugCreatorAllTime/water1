using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceVibration
{
    private const string Key = "DeviceVibrationEnable";

    static bool isEnable;

    public static bool IsEnable
    {
        get
        {
            return isEnable;
        }

        set
        {
            ///
            PlayerPrefs.SetInt(Key, value ? 1 : 0);
            isEnable = value;
        }
    }

    static DeviceVibration()
    {
        isEnable = PlayerPrefs.GetInt(Key, 1) == 1;
    }

    public static void TryVibrate()
    {
        if (IsEnable)
        {
            Handheld.Vibrate();
        }
    }
}
