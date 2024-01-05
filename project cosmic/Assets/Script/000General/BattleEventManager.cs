using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//전투 관련 룸 불러오는 거랑 적 불러오는거 관리.
public class BattleEventManager : MonoBehaviour
{
    GameObject enemyPoolParent;


    //enemy
    public List<EnemyData> enemyDataList = new List<EnemyData>();
    public GameObject enemyBullet;


    private List<GameObject> enemyPool = new List<GameObject>();




    void Start()
    {
        enemyPoolParent = GameObject.Find("EnemyPoolParent");

        InitializeEnemyPool();  //스테이지 무관하게 호출
    }

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

        gameObject.GetComponent<PlayerInfo>().APModify(1);

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