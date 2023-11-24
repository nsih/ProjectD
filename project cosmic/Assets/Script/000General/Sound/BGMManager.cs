using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource bgmAudioSource;
    public List<AudioClip> bgmList;


    private int currentBGMIndex = -1;
    [Range(0f, 1f)]
    public float bgmVolume = 1f;

    private void Awake()
    {
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;


        InitializeBGMList();
        //PlayBGM(BGMType.Legend);
    }

    public void PlayBGM(BGMType bgmType)
    {
        int index = (int)bgmType;


        if (index >= 0 && index < bgmList.Count)
        {
            if (index != currentBGMIndex)
            {
                currentBGMIndex = index;
                bgmAudioSource.clip = bgmList[currentBGMIndex];
                bgmAudioSource.volume = 0.1f;

                bgmAudioSource.time = 62;
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


    void InitializeBGMList()
    {
        bgmList.Add(Resources.Load<AudioClip>("Sound/BGM/Legend"));
    }
}

public enum BGMType
{
    Legend
}
