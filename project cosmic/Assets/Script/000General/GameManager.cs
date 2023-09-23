using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    private float previousTimeScale;


    public static int currentStage;

    public static int playerLocationX;
    public static int playerLocationY;


    public static bool isEventEnd;

    
    public static bool isLoading;


    public static bool isQuestDone;


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


    public static GameManager Instance
    {
        get { return instance; }
    }


    public void PauseGame()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        Time.timeScale = previousTimeScale; // 이전 속도로 복구
    }



    public void OpenNewStage()
    {
        //initialize Stage
        currentStage++;

        playerLocationX = 0;
        playerLocationY = 0;

        GetComponent<mapGenerator>().GenerateMap(6,18);
        PlayerLocationReset();
    }

    public void OpenNewRoom()
    {
        //플레이어위치, 액션 스택, 정신력 , 퀘스트 확인

        PlayerLocationReset();
        GetComponent<PlayerInfo>().sanityModify(-1);
    }



    void PlayerLocationReset()
    {
        GameObject player;

        if(GameObject.Find("player") != null)
        {
            player = GameObject.Find("player");

            player.transform.position = new Vector3 (0,0,0);
        }
    }
}
