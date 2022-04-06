using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SL
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicEffectController : MonoBehaviour
    {
        public static MusicEffectController InstanceOfMusic=null;
        AudioSource audioSource;

        // Use this for initialization
        void Awake(){
                if(InstanceOfMusic==null){
                    InstanceOfMusic=this;
                }
                else if(InstanceOfMusic!=this){
                    Destroy(gameObject);
                }
        }
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = !SoundManager.MusicOn;
            SoundManager.OnMusicSettingChange += SoundManager_OnSoundSettingChange;
        }

        void SoundManager_OnSoundSettingChange()
        {
            audioSource.mute = !SoundManager.MusicOn;
        }

        public void OnDestroy()
        {
            SoundManager.OnMusicSettingChange -= SoundManager_OnSoundSettingChange;
        }
    }
}
