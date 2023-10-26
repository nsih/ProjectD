using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    public EnemyData enemyData;

    GameObject gameManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    void OnEnable()
    {
        StartCoroutine(GetMob());
    }

    IEnumerator GetMob()
    {
        yield return new WaitForSeconds(1.5f);

        gameManager.GetComponent<BattleEventManager>().PickMob(this.gameObject);
    }
}
