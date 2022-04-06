using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicFxToggle : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;

    public void OnEnable()
    {
        toggle.SetIsOnWithoutNotify(SL.SoundManager.MusicOn);
    }

    public void OnValueChanged()
    {
        SL.SoundManager.MusicOn = toggle.isOn;
    }
}
