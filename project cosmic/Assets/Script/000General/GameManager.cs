using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;    //돌릴 인스턴스


    
    public static int currentStage;
    public static int currentRoom;

    public static int actionStack;
    public static int doomCount;

    public static bool isEncounterPhase;
    public static bool isActionPhase;


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


    void Start()
    {
        currentStage = 0;
        currentRoom = 0;

        actionStack = 1;
    }


    public static GameManager Instance
    {
        get { return instance; }
    }


    void CheckStageQuest()
    {
        //스테이지 마다 다른 퀘스트 진행도 체크. 
        //완료했으면 이동버튼 변경.
    }

    void CheckDoomCount()
    {
        if(doomCount == 0 && actionStack == 0 && isActionPhase) //&&퀘스트 완료 여부
        {
            Debug.Log("GameOver");
            
        }
    }


    public void ActionStackInitialize()
    {
        GameObject player;

        if(GameObject.Find("player") != null)
        {
            player = GameObject.Find("player");

            actionStack = player.GetComponent<PlayerInfo>().actionLimit;
        }
    }

    public void ActionStackModify(int modifier)
    {
        int changedActionStack = actionStack+modifier;

        if (changedActionStack <= 0)
        {
            actionStack = 0;
        }
        else
        {
            actionStack = changedActionStack;
        }
    }




    public void DoomCountInitialize()
    {
        if(currentStage == 0)
        {
            doomCount = 7;
        }

        else
        {
            Debug.Log(currentStage);
        }
    }

    public void DoomCountModify(int modifier)
    {
        int changedDoomCount = doomCount+modifier;

        if (changedDoomCount <= 0)
        {
            doomCount = 0;
        }
        else
        {
            doomCount = changedDoomCount;
        }
    }
}
