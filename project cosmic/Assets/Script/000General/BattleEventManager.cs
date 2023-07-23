using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventManager : MonoBehaviour
{
    
    public GameObject[] battleRoomArray = new GameObject[3];



    public EnemyData[] enemyDatas = new EnemyData[3];
    public GameObject[] enemyArray = new GameObject[3];
    public List<GameObject> enemyPool = new List<GameObject>(); 



    void InitializeEnemyPool()    //비어있는 풀에 몹을 잰하는.. feat enemy data
    {
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject enemy = Instantiate(enemyArray[i]);

                enemy.GetComponent<EnemyCon>().enemyData = enemyDatas[i];

                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
        }
    }

    void ClearEnemyPool()
    {
        enemyPool.Clear();
    }

    public bool isPoolAllDisable()  //끝 체크할거
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeSelf)
            {
                return true;
            }
        }
        return false;
    }


    public void GenMob(int stage)
    {
        if(stage == 0)
        {


        }

        else if(stage == 1)
        {


        }
    }


}
