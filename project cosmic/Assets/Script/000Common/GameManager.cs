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
    public static bool isMovingPhase;

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

    void StartRoomEventPhase()
    {
        SetRoomEventPhase();

        //currentRoom search true로 바꿈.

        //if()
        //null              : 바로 action Phase로
        //전투              : 몹 잰, 다 잡으면 넘어감 
        //시련              : 팝업 띄우면서 시련 시작 
        //스테이지 이벤트    : 나도 모름 
        //상점 재단         : 상점 오브젝트 배치 
        //보스              : 대화 + 시련 + 전투 (순서는 각각) 
    } 
    void StartActionPhase()
    {
        SetActionPhase();
        //버튼 활성화
    }
    void StartMovingPhase()
    {
        SetMovingPhase();
        //이동 외 버튼 비활성화
    }

    void SetRoomEventPhase()
    {
        isRoomEventPhase = true;
        isActionPhase = false;
        isMovingPhase = false;
    }
    void SetActionPhase()
    {
        isRoomEventPhase = false;
        isActionPhase = true;
        isMovingPhase = false;
    }
    private void SetMovingPhase()
    {
        isRoomEventPhase = false;
        isActionPhase = false;
        isMovingPhase = true;
    }
    #endregion
}
