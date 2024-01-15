using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    AudioSource sfxAudioSource;

    public List<AudioClip> sfxList;


    void Awake()
    {
        sfxAudioSource = gameObject.AddComponent<AudioSource>();

        // 여기에서 sfxAudioSource의 설정을 확인 및 조절
        sfxAudioSource.spatialBlend = 0; // 예시로 spatialBlend를 2D로 설정
        sfxAudioSource.minDistance = 1; // 예시로 최소 거리 설정
        sfxAudioSource.maxDistance = 10; // 예시로 최대 거리 설정

        InitializeSFXList();
    }

    public void PlaySound(SfxType sfxType)
    {
        int index = (int)sfxType;
        

        if (index >= 0 && index < sfxList.Count)
        {
            sfxAudioSource.time = 0.05f;
            sfxAudioSource.PlayOneShot(sfxList[index]);
            //Debug.Log("asd");
        }
        else
        {
            Debug.LogError("Invalid sound index");
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }

    public void StopSFX()
    {
        sfxAudioSource.Stop();
    }


    //enum 이랑 순서가 다르면 큰일나요, enum 이랑 리소스 이름이 다르면 큰일나요
    void InitializeSFXList()
    {
        sfxList = new List<AudioClip>();

        sfxList.Add(Resources.Load<AudioClip>("Sound/SFX/BtnHover"));
        sfxList.Add(Resources.Load<AudioClip>("Sound/SFX/BtnClick"));
        sfxList.Add(Resources.Load<AudioClip>("Sound/SFX/DialogueTyping"));

        //sfxList.Add(Resources.Load<AudioClip>("Sound/SFX/NAME"));
    }
}

public enum SfxType
{
    BtnHover,
    BtnClick,
    DialogueTyping
}