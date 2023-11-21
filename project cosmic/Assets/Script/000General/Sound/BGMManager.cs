using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource bgmAudioSource;
    public AudioClip[] bgmClips;
    private int currentBGMIndex = -1;
    [Range(0f, 1f)]
    public float bgmVolume = 1f;

    private void Awake()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        //PlayBGM(0);
    }

    public void PlayBGM(int bgmIndex)
    {
        if (bgmIndex >= 0 && bgmIndex < bgmClips.Length)
        {
            if (bgmIndex != currentBGMIndex)
            {
                currentBGMIndex = bgmIndex;
                bgmAudioSource.clip = bgmClips[currentBGMIndex];
                bgmAudioSource.volume = bgmVolume;
                bgmAudioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Invalid BGM index");
        }
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
        currentBGMIndex = -1;
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmAudioSource.volume = bgmVolume;
    }
}
