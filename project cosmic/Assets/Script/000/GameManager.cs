using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random=UnityEngine.Random;


//게임흐름과 오브젝트배치 관리
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    GameObject canvas;



    private float previousTimeScale;


    public static int currentStage;

    public static int playerLocationX;
    public static int playerLocationY;


    public static bool isEventEnd;
    public static bool isLoading;

    public static bool isRoomTalking;
    public static bool isLandTalking;

    public static bool isTesting;


    private void Awake()
    {
        //singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentStage = 0;
        playerLocationX = 0;
        playerLocationY = 0;

        isEventEnd = false;
        isLoading = false;
        isTesting = false;

        isRoomTalking = false;
        isLandTalking = false;


        //OpenNewStage();//임시
    }


    public static GameManager Instance
    {
        get { return instance; }
    }



    /// <summary>
    /// //////////////////////
    /// </summary>


    public void PauseGame()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        Time.timeScale = previousTimeScale; // 이전 속도로 복구
    }


    void PlayerLocationReset()
    {
        GameObject player;

        if (GameObject.Find("player") != null)
        {
            player = GameObject.Find("player");

            player.transform.position = new Vector3(0, 0, 0);
        }
    }


}