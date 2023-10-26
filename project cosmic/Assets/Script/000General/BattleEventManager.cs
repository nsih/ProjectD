using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventManager : MonoBehaviour
{
    GameObject battleRoomPoolParent;
    GameObject enemyPoolParent;

    //room

    //준비된 프리팹 리스트
    public List<GameObject> RoomList1 = new List<GameObject>();


    //로직에서 쓸거
    public List<RoomData> roomData = new List<RoomData>();


    //enemy
    public List<EnemyData> enemyDataList = new List<EnemyData>();
    private List<GameObject> enemyPool = new List<GameObject>();

    void Start()
    {
        battleRoomPoolParent = GameObject.Find("BattleRoomPoolParent");
        enemyPoolParent = GameObject.Find("EnemyPoolParent");

        InitializeEnemyPool();  //스테이지 무관하게 호출
    }

    #region "room Pool"

    //스테이지 새로열리면 리스트 비우고 해당 스테이지 방 개수만큼 생성
    //스테이지 갱신마다 호출ㄹㄹ
    void InitializeRoomUsedData()
    {
        roomData.Clear();

        switch (GameManager.currentStage)
        {
            case 1:
                for (int i = 0; i < RoomList1.Count; i++)
                {
                    roomData[i].roomOBJ = RoomList1[i];
                    roomData[i].isUsed = true;
                }
                break;


            default:
                Debug.Log("stage error");
                break;
        }
    }

    void InitializeRoomPool() //
    {
        //랜덤으로 뽑아서 roomUsed[]에 false면 다시 뽑고 해당 list 데이터 불러오고 시작
    }

    #endregion

    #region "enemy Pool"
    void InitializeEnemyPool() //적데이터당 10마리씩 적 인스턴스 생성
    {
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject enemy = Instantiate(enemyDataList[i].enemyGameObject, enemyPoolParent.transform);

                enemy.GetComponent<EnemyManager>().enemyData = enemyDataList[i];

                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
        }
    }

    void ClearEnemyPool()
    {
        enemyPool.Clear();
    }

    public bool isPoolAllDone()  //해치웠나? 체크
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeSelf)
            {
                return false;
            }
        }
        return true;
    }


    public void PickMob(GameObject sCircle)
    {
        foreach (GameObject var in enemyPool)
        {
            if (var.GetComponent<EnemyManager>().enemyData == sCircle.GetComponent<SummonEnemy>().enemyData && var.activeSelf == false)
            {
                var.transform.position = sCircle.transform.position;
                var.SetActive(true);

                sCircle.SetActive(false);

                break;
            }
        }
    }

    #endregion


}

public class RoomData
{
    public GameObject roomOBJ;
    public bool isUsed;
}