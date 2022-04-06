using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SL
{
    public static class SoundManager
    {
        private const string SOUND_FX_KEY = "EffectOn";
        private const string MUSIC_KEY = "MusicOn";

        public static event System.Action OnSoundSettingChange;
        public static event System.Action OnMusicSettingChange;

        public static bool EffectOn
        {
            get
            {
                return effectOn;
            }
            set
            {
                effectOn = value;
                PlayerPrefs.SetInt(SOUND_FX_KEY, effectOn ? 1 : 0);
                PlayerPrefs.Save();
                if (OnSoundSettingChange != null)
                {
                    OnSoundSettingChange();
                }
            }
        }

        public static bool MusicOn
        {
            get
            {
                return musicOn;
            }
            set
            {
                musicOn = value;
                PlayerPrefs.SetInt(MUSIC_KEY, musicOn ? 1 : 0);
                PlayerPrefs.Save();
                if (OnMusicSettingChange != null)
                {
                    OnMusicSettingChange();
                }
            }
        }

        static bool effectOn = true;
        static bool musicOn = true;

        static SoundManager()
        {
            effectOn = PlayerPrefs.GetInt(SOUND_FX_KEY, 1) == 1;
            musicOn = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
        }
    }
}
