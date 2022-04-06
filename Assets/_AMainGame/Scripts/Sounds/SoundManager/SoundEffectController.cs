using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SL
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectController : MonoBehaviour
    {
        AudioSource audioSource;

        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = !SoundManager.EffectOn;
            SoundManager.OnSoundSettingChange += SoundManager_OnSoundSettingChange;
        }

        void SoundManager_OnSoundSettingChange()
        {
            audioSource.mute = !SoundManager.EffectOn;
        }

        public void OnDestroy()
        {
            SoundManager.OnSoundSettingChange -= SoundManager_OnSoundSettingChange;
        }
    }
}
