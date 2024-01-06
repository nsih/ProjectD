using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class EnemyBulletPoolManager : MonoBehaviour
{
    GameObject gameManager;
    GameObject player;

    public List<GameObject> enemyBulletPool = new List<GameObject>();



    void Start()
    {
        InitializeEnemyBulletPool();
    }



    public void InitializeEnemyBulletPool()
    {
        gameManager =  GameObject.Find("GameManager");
        player = GameObject.Find("player");

        enemyBulletPool.Clear();

        for (int i = 0; i < 250; i++)
        {
            GameObject var 
            = Instantiate( gameManager.GetComponent<BattleEventManager>().enemyBullet, gameObject.transform.Find("EnemyBulletPoolParent").gameObject.transform);
            var.SetActive(false);
            enemyBulletPool.Add(var);
        }
    }

    public void AddEnemyBulletPool()
    {
        for (int i = 0; i <= 100; i++)
        {
            GameObject var 
            = Instantiate( gameManager.GetComponent<BattleEventManager>().enemyBullet, gameObject.transform.Find("EnemyBulletPoolParent").gameObject.transform);
            var.SetActive(false);
            enemyBulletPool.Add(var);
        }
    }
}
