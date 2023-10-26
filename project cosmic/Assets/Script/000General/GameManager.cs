using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    GameObject landUICanvas;
    
    private float previousTimeScale;


    public static int currentStage;

    public static int playerLocationX;
    public static int playerLocationY;


    public static bool isEventEnd;
    public static bool isLoading;


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


        OpenNewStage();//임시
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



    public void OpenNewStage()
    {
        landUICanvas = GameObject.Find("LandUICanvas");

        //initialize Stage
        currentStage++;

        playerLocationX = 0;
        playerLocationY = 0;

        GetComponent<mapGenerator>().GenerateMap(6,18);         //생성
        
        landUICanvas.GetComponent<MapDrawer>().UpdateDrawMap(); //그림



        PlayerLocationReset();
    }

    public void OpenNewRoom()
    {
        PlayerLocationReset();
        GetComponent<PlayerInfo>().SanityModify(-1);
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
