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

    GameObject landUICanvas;

    GameObject roomParent;


    //room
    //준비된 프리팹 리스트
    public List<LoadingRoomData> roomDataList1 = new List<LoadingRoomData>();

    List<LoadingRoomData> currentStageRoomDataList = new List<LoadingRoomData>();


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



    public void OpenNewStage()
    {
        landUICanvas = GameObject.Find("LandUICanvas");
        roomParent = GameObject.Find("RoomParent");

        //initialize Stage
        currentStage++;

        playerLocationX = 0;
        playerLocationY = 0;

        GetComponent<mapGenerator>().GenerateMap(6, 18);         //맵 생성
        landUICanvas.GetComponent<MapDrawer>().UpdateDrawMap(); //맵 그림
        landUICanvas.GetComponent<MapDrawer>().map.SetActive(false);

        InitializeRoomData();

        PlayerLocationReset();

        this.gameObject.GetComponent<TestEventManager>().InitializeRandomEventIsTested(currentStage);

        OpenNewRoom(RoomType.Start);
    }

    public void OpenNewRoom(RoomType roomType)
    {
        switch(roomType)
        {
            case RoomType.Start:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.Boss:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.Battle:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.EliteBattle:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.FixedEvent:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.RandomEvent:
                PickupRoom(RoomType.RandomEvent);
                
                gameObject.GetComponent<TestEventManager>().StartRandomTestEvent(currentStage);
                //start event
                break;

            case RoomType.Alter:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.Shop:
                PickupRoom(RoomType.Battle);
                break;

            case RoomType.NPC:
                PickupRoom(RoomType.Battle);
                break;
        }

        PlayerLocationReset();


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


    #region "room Pool Control"

    //스테이지마다 Room Data 초기화, 인스턴스 생성
    public void InitializeRoomData()
    {
        //전 스테이지의 오브젝트 풀 삭제
        foreach (Transform child in roomParent.transform)
        {
            Destroy(child.gameObject);
        }

        //스테이지에 따라 데이터의 풀 가져오기
        switch (currentStage)
        {
            case 1:
                for (int i = 0; i < roomDataList1.Count; i++)
                {
                    GameObject room = Instantiate(roomDataList1[i].roomOBJ, roomParent.transform);
                    roomDataList1[i].isUsed = false;
                }

                currentStageRoomDataList = roomDataList1;
                break;


            default:
                Debug.Log("stage error : "+currentStage);
                break;
        }
    }
    
    //원하는 타입의 방 뽑기
    public void PickupRoom(RoomType roomType)
    {
        int randomNumber;
        bool roomPicked = false;

        //DisableAllRooms
        foreach (Transform child in roomParent.transform)
        {
            child.gameObject.SetActive(false);
        }
        
        //룸타입이 일치하며 사용되지 않은 방을 뽑을때까지 랜덤
        while (!roomPicked)
        {
            randomNumber = Random.Range(0, currentStageRoomDataList.Count);
            
            if (!currentStageRoomDataList[randomNumber].isUsed && 
            currentStageRoomDataList[randomNumber].roomType == roomType)
            {
                currentStageRoomDataList[randomNumber].isUsed = true;
                roomParent.transform.GetChild(randomNumber).gameObject.SetActive(true);
                roomPicked = true;
            }
        }
    }

    #endregion
}


[Serializable]
public class LoadingRoomData
{
    public GameObject roomOBJ;
    public RoomType roomType;
    public bool isUsed;
}
