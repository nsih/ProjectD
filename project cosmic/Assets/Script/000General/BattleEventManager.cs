using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventManager : MonoBehaviour
{
    public GameObject[] enemyArray = new GameObject[3];
    public GameObject[] battleRoomArray = new GameObject[1];


    public List<GameObject> enemyPool = new List<GameObject>(); 






    int generateFlag;

    void Start()
    {
        
    }



    void GenerateEnemyPool()
    {
        for (int i = 0; i < enemyArray.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject enemy = Instantiate(enemyArray[i]);
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


}


public class BattleData
{
    public int battleNum;     //index

    public BattleData(int battleNum,  bool isRevealed, bool isTp,bool isClear)
    {
        this.battleNum = battleNum;
    }


    public void GenMob(int stage)
    {
        if(stage == 0)
        {
            
        }
    }
}
