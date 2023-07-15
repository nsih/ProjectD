using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;    //돌릴 인스턴스


    
    public static int currentStage;
    public static int currentRoom;
    public static int doomCount;

    public static bool isRoomEventPhase;
    public static bool isActionPhase;

    void Start()
    {
        currentStage = 0;
        currentRoom = 0;
    }




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





    
    #region "System - Stage"

    void GameInitialize()
    {
        //겜오버 or 끝낼떄?
    }
    void StageInitialize()
    {
        currentStage++; //0->1

        //Map Initialize

        //Map Visualization

        //DoomCount Initialize

        //Quest Initialize

        //player Object Location
    }

    void CheckPhase()
    {
        //Phase 체크해서 행동제한.
    }

    void CheckStageQuest()
    {
        //스테이지 마다 다른 퀘스트 진행도 체크. 
        //완료했으면 이동버튼 변경.
    }

    void DoomCountModify(int modifier)
    {
        int changedDoomCount = doomCount+modifier;

        if (changedDoomCount <= 0)
        {
            Debug.Log("game over DoomCount");
        }
        else
        {
            doomCount = changedDoomCount;
        }
    }

    #endregion

    #region "System - Room"

    void SetRoomEventPhase()
    {
        isRoomEventPhase = true;
        isActionPhase = false;
    }
    void SetActionPhase()
    {
        isRoomEventPhase = false;
        isActionPhase = true;
    }



    void StartRoomEventPhase()
    {
        SetRoomEventPhase();

        if(StageManager.map[currentRoom].roomType == RoomType.Null)
        {
            
        }

        else if(StageManager.map[currentRoom].roomType == RoomType.Battle)
        {

        }

        else if(StageManager.map[currentRoom].roomType == RoomType.Test)
        {

        }

        else if(StageManager.map[currentRoom].roomType == RoomType.Altar)
        {
            
        }

        else if(StageManager.map[currentRoom].roomType == RoomType.Shop)
        {
            
        }

        else if(StageManager.map[currentRoom].roomType == RoomType.Event)
        {
            
        }

        else if(StageManager.map[currentRoom].roomType == RoomType.Boss)
        {
            
        }

        else
        {
            Debug.Log(StageManager.map[currentRoom].roomType);
        }
    }




    void StartActionPhase()
    {
        SetActionPhase();   
    }

    #endregion
}
