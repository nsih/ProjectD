using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventManager : MonoBehaviour
{
    GameObject battleRoomPoolParent;
    GameObject enemyPoolParent;


    public List<GameObject>battleRoomList = new List<GameObject>();
    private List<GameObject> battleRoomPool = new List<GameObject>(); 



    public List<EnemyData> enemyDataList = new List<EnemyData>();
    private List<GameObject> enemyPool = new List<GameObject>(); 

    void Start ()
    {
        battleRoomPoolParent = GameObject.Find("BattleRoomPoolParent");
        enemyPoolParent = GameObject.Find("EnemyPoolParent");

        InitializeEnemyPool();  //스테이지 무관하게 호출
    }

    #region "room Pool"
    
    #endregion

    #region "enemy Pool"
    void InitializeEnemyPool() //적데이터당 10마리씩 적 인스턴스 생성
    {
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject enemy = Instantiate(enemyDataList[i].enemyGameObject,enemyPoolParent.transform);

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
        foreach(GameObject var in enemyPool)
        {
            if(var.GetComponent<EnemyManager>().enemyData == sCircle.GetComponent<SummonEnemy>().enemyData && var.activeSelf == false)
            {
                var.transform.position = sCircle.transform.position;
                var.SetActive(true);

                sCircle.SetActive(false);

                break;
            }
        }
    }


    public void GenBattleRoom(int stage)
    {
        int roomFlag = Random.Range(0,1);   //0만나옴

        switch (stage)
        {
            case 0:

            switch (roomFlag)
            {
                case 0:
                break;

                case 1:
                break;

                case 2:
                break;

                case 3:
                break;

                case 4:
                break;
            }

            break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            default:
                Debug.Log(stage);
                break;
        }
    }

    #endregion


}
