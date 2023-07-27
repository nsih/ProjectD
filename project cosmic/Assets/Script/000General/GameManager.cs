using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;    //돌릴 인스턴스

    GameObject managerCanvas;
    GameObject loadingScreen;

    private bool isPaused = false;
    private float previousTimeScale;


    
    public static float loadingTime;


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
        loadingTime = 1.0f;
    }


    public static GameManager Instance
    {
        get { return instance; }
    }


    public void PauseGame()
    {
        isPaused = true;
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = previousTimeScale; // 이전 속도로 복구
    }



    public void OpenNewStage()
    {
        //initialize Stage

        Start();

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


    public void StartLoading()
    {
        loadingScreen = GameObject.Find("ManagerCanvas").gameObject.transform.GetChild(0).gameObject;

        loadingScreen.SetActive(true); // 오브젝트를 활성화시킵니다.
        isLoading = true;

        Debug.Log("부켜조");
    }

    public void EndLoading()
    {
        loadingScreen = GameObject.Find("ManagerCanvas").gameObject.transform.GetChild(0).gameObject;

        loadingScreen.SetActive(false); // 오브젝트를 비활성화시킵니다.
        isLoading = false;

        Debug.Log("부꺼조");
    }

    /*
    public void GoLoading()
    {
        StartCoroutine("Loading");
    }


    IEnumerator Loading()
    {
        

        StartLoading();
        PauseGame();

        yield return new WaitForSeconds(2.0f);


        EndLoading();
        ResumeGame();

        yield return null;
    }
    */
}
