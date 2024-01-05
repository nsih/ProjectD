using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class EnemyBulletPoolManager : MonoBehaviour
{
    GameObject gameManager;
    GameObject player;

    public List<GameObject> enemyBulletPool = new List<GameObject>();



    public void InitializeEnemyBulletPool(GameObject enemy)
    {
        gameManager =  GameObject.Find("GameManager");
        player = GameObject.Find("player");

        enemyBulletPool.Clear();

        for (int i = 0; i <= 35; i++)
        {
            GameObject var = Instantiate( gameManager.GetComponent<BattleEventManager>().enemyBullet, enemy.transform);
            var.SetActive(false);
            enemyBulletPool.Add(var);
        }
    }

    public void AddEnemyBulletPool()
    {

    }
}
