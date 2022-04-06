using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFxToggle : MonoBehaviour
{

    [SerializeField]
    private Toggle toggle;

    public void OnEnable()
    {
        toggle.SetIsOnWithoutNotify(SL.SoundManager.EffectOn);
    }

    public void OnValueChanged()
    {
        SL.SoundManager.EffectOn = toggle.isOn;
    }
}
