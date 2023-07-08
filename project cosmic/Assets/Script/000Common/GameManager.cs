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





    
    #region "System"
    void StartStage()
    {

    }

    void EndStage()
    {

    }

    void CheckPhase()
    {
        //Phase 체크해서 행동제한.
    }

    void CheckStageQuest()
    {
        //스테이지 퀘스트 진행도 체크. 
        //완료했으면 이동버튼 변경.
    }

    void CheckDoomCount()
    {
        //조작될때 카운터 수치 확인
    }

    #endregion
}
