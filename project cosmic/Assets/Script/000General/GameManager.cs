using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


//게임흐름과 오브젝트배치 관리
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

        GetComponent<mapGenerator>().GenerateMap(6, 18);         //생성
        landUICanvas.GetComponent<MapDrawer>().UpdateDrawMap(); //그림
        landUICanvas.GetComponent<MapDrawer>().map.SetActive(false);

        PlayerLocationReset();
    }

    public void OpenNewRoom(RoomType roomType)
    {
        switch(roomType)
        {
            case RoomType.Start:
            //스타트 이벤트
                break;

            case RoomType.Boss:
            //맵생성 -> 보스 이벤트 -> 전투
                break;

            case RoomType.Battle:
            //맵생성 -> 전투
                break;

            case RoomType.EliteBattle:
            //맵생성 -> 전투
                break;

            case RoomType.FixedEvent:
            //UI
                break;

            case RoomType.RandomEvent:
            //UI
                break;

            case RoomType.Alter:
            //맵생성
                break;

            case RoomType.Shop:
            //맵생성
                break;

            case RoomType.NPC:
            //맵생성 랜덤이벤트에서 나올수도 안나올수도
                break;
        }

        PlayerLocationReset();
        GetComponent<PlayerInfo>().SanityModify(-1);


        //룸타입에 따라서 룸 꺼내오는 함수  -> battle event manager
        //룸타입에 따라서 이벤트 꺼내오는 함수
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
