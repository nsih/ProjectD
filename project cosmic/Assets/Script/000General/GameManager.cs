using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;


    GameObject loadingScreen;
    
    private float previousTimeScale;


    
    public static float loadingTime;


    public static int currentStage;
    public static int currentRoom;

    public static int actionStack;
    public static int mentality;

    public static int coin;


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

        Start();

        currentStage++;
        currentRoom = 0;

        GetComponent<StageManager>().GenerateNewStage();

        mentalityInitialize();
        PlayerLocationReset();
        ActionStackReset();


    }

    public void OpenNewRoom()
    {
        //플레이어위치, 액션 스택, 정신력 , 퀘스트 확인

        PlayerLocationReset();                
        ActionStackReset();
        mentalityModify(-1);
        GetComponent<StageManager>().AddTpConnect();

        GetComponent<StageManager>().CheckStageQuest();
    }


    void CheckStageQuest()
    {
        //스테이지 마다 다른 퀘스트 진행도 체크. 
        //완료했으면 이동버튼 변경.
    }

    void Checkmentality()
    {
        if(mentality == 0 && isActionPhase && actionStack == 0) //&&퀘스트 완료 여부
        {
            Debug.Log("GameOver");
        }
    }


    public void ActionStackReset()
    {
        actionStack = GetComponent<PlayerInfo>().actionStackLimit;
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




    public void mentalityInitialize()
    {
        if(currentStage == 0)
        {
            mentality = 8;
        }

        else
        {
            Debug.Log(currentStage);
        }
    }

    public void mentalityModify(int modifier)
    {
        int changedmentality = mentality+modifier;

        if (changedmentality <= 0)
        {
            mentality = 0;
        }
        else
        {
            mentality = changedmentality;
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

        loadingScreen.SetActive(true); 
        isLoading = true;
    }

    public void EndLoading()
    {
        loadingScreen = GameObject.Find("ManagerCanvas").gameObject.transform.GetChild(0).gameObject;

        loadingScreen.SetActive(false);         
        isLoading = false;
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
