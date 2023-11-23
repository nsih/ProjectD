using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource sfxAudioSource;


    //버튼 호버, 버튼 클릭
    public List<AudioClip> sfxClipsButton;

    public List<AudioClip> sfxClipsDialogue;

    void Awake()
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
