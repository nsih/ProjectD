using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;    //돌릴 인스턴스

    GameObject managerCanvas;
    GameObject loadingScreen;


    
    public static int currentStage;
    public static int currentRoom;

    public static int actionStack;
    public static int doomCount;

    public static bool isEncounterPhase;
    public static bool isActionPhase;
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


    void Start()
    {
        currentStage = -1;
        
        loadingScreen = GameObject.Find("LoadingScreen");
        loadingScreen.SetActive(false);
    }


    public static GameManager Instance
    {
        get { return instance; }
    }



    public void OpenNewStage()
    {
        //initialize Stage

        ShowLoadingScreen();

        currentStage++;
        currentRoom = 0;

        GetComponent<StageManager>().GenerateNewStage();

        DoomCountInitialize();
        PlayerLocationReset();
        ActionStackReset();


    }

    public void OpenNewRoom()
    {
        //initialize Room

        PlayerLocationReset();
        ActionStackReset();
        DoomCountModify(-1);
        GetComponent<StageManager>().AddTpConnect();

        GetComponent<StageManager>().CheckStageQuest();
    }


    public void SetEncounterPhase()
    {
        isEncounterPhase = true;
        isActionPhase = false;
    }

    public void SetActionPhase()
    {
        isEncounterPhase = false;
        isActionPhase = true;
    }


    void CheckStageQuest()
    {
        //스테이지 마다 다른 퀘스트 진행도 체크. 
        //완료했으면 이동버튼 변경.
    }

    void CheckDoomCount()
    {
        if(doomCount == 0 && isActionPhase && actionStack == 0) //&&퀘스트 완료 여부
        {
            Debug.Log("GameOver");
        }
    }


    public void ActionStackReset()
    {
        /*
        GameObject player;

        if(GameObject.Find("player") != null)
        {
            player = player.GameObject.Find("player");

            actionStack = GetComponent<PlayerInfo>().actionLimit;
        }
        */
        actionStack = GetComponent<PlayerInfo>().actionLimit;
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
            doomCount = 8;
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



    void PlayerLocationReset()
    {
        GameObject player;

        if(GameObject.Find("player") != null)
        {
            player = GameObject.Find("player");

            player.transform.position = new Vector3 (0,0,0);
        }
    }



    public void ShowLoadingScreen()
    {
        StartCoroutine(LoadingScreen());
    }

    private IEnumerator LoadingScreen()
    {
        Debug.Log("Start activating the object");
        loadingScreen.SetActive(true); // 오브젝트를 활성화시킵니다.
        isLoading = true;

        yield return new WaitForSeconds(1.0f);

        loadingScreen.SetActive(false); // 오브젝트를 비활성화시킵니다.
        isLoading = false;

        Debug.Log("Object activation finished");
    }
}
