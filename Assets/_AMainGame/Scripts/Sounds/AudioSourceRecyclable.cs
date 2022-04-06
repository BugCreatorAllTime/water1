using FH.Core.Architecture.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceRecyclable : GeneralPoolMember
{
    private AudioSource audioSource;

    public void Play(AudioClip audioClip, float pitch, float volume)
    {
        ///
        gameObject.SetActive(true);

        ///
        TryGetAudioSource();

        ///
        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        audioSource.volume = volume;

        ///
        audioSource.Play();
    }

    private void TryGetAudioSource()
    {
        ///
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void Update()
    {
        ///
        TryGetAudioSource();

        ///
        if (!audioSource.isPlaying)
        {
            TryReturnToPoolAndDeactivate();
        }
    }

#if UNITY_EDITOR
    private void Reset()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }
#endif
}
