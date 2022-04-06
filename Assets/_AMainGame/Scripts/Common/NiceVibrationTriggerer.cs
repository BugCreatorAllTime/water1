using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class NiceVibrationTriggerer : MonoBehaviour
{
    [SerializeField]
    private HapticTypes hapticType;

    public void Haptic(HapticTypes type)
    {
        if (DeviceVibration.IsEnable)
        {
            MMVibrationManager.Haptic(type);
        }
    }

    public void Haptic()
    {
        Haptic(hapticType);
    }
}
