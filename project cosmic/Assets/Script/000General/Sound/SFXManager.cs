using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource sfxAudioSource;

    private void Awake()
    {
        sfxAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxAudioSource.PlayOneShot(sfxClip);
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = Mathf.Clamp01(volume);
    }

    public void StopSFX()
    {
        sfxAudioSource.Stop();
    }
}
