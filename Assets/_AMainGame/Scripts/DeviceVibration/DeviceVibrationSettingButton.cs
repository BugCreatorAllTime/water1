using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceVibrationSettingButton : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;

    public void OnEnable()
    {
        toggle.SetIsOnWithoutNotify(DeviceVibration.IsEnable);
    }

    public void OnValueChanged()
    {
        DeviceVibration.IsEnable = toggle.isOn;
    }
}
