using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject gameManager;

    GameObject player;


    public EnemyData enemyData;

    public string mobName;

    float hp;
    int damage;
    float moveSpeed;
    float attackDelay;
    float bulletSpeed;



    bool isKnockBack;
    float knockBackDuration = 0.05f;
    float knockBackSpeed = 15f;




    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");
    }

    void OnEnable()
    {
        InitializeEnemyStatus();

        StartCoroutine(RunBehaviorPatterns());
    }

    void OnDisable()
    {
        if (gameManager.GetComponent<BattleEventManager>().isPoolAllDone())
        {
            GameManager.isEventEnd = true;
        }
    }


    void InitializeEnemyStatus()
    {
        this.gameObject.tag = "Enemy";

        mobName = enemyData.mobName;

        hp = enemyData.hp;
        damage = enemyData.damage;
        moveSpeed = enemyData.moveSpeed;

        attackDelay = enemyData.attackDelay;
        bulletSpeed = enemyData.bulletSpeed;

        isKnockBack = false;
        //
    }



    #region "collision (Been attacked)"
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerAttack")
        {
            //Debug.Log("HP : "+hp);
            HandlePlayerStrike(other.gameObject);

            other.gameObject.SetActive(false);
            //other.gameObject.GetComponent<PlayerBulletCon>().VanishOnCollision();
        }

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("asd");
            //HandlePlayerStrike();
        }
    }

    void HandlePlayerStrike(GameObject hit)   //공격받았을때 Hp를 줄이고 맞은 탄을 소거합니다.
    {
        KnockBack(hit);
        if ((hp - PlayerInfo.playerDMG) > 0)
        {
            hp = hp - PlayerInfo.playerDMG;

            //Debug.Log("PlayerInfo.playerDMG : "+PlayerInfo.playerDMG);
            //Debug.Log("Changed HP : "+hp);
        }

        else
        {
            hp = 0;
            this.gameObject.SetActive(false);
            //Debug.Log(this.gameObject.name);
        }
    }

    void KnockBack(GameObject hit)
    {
        if (enemyData.knockBackable && !isKnockBack)
        {
            isKnockBack = true;
            Vector3 knockBackDirection = -(hit.transform.position - transform.position).normalized; //oposit of hit object
            StartCoroutine(DoKnockBack(knockBackDirection));
        }
    }

    IEnumerator DoKnockBack(Vector3 knockBackDirection)
    {
        float timer = 0f;
        while (timer < knockBackDuration)
        {
            timer += Time.deltaTime;
            transform.position += knockBackDirection * knockBackSpeed * Time.deltaTime;
            yield return null;
        }
        isKnockBack = false;
    }
    #endregion


    #region "Behaivior pattern"

    //패턴 관리
    IEnumerator RunBehaviorPatterns()
    {
        while (true)
        {
            foreach (var pattern in enemyData.behaviorPatterns)
            {
                switch (pattern.actionType)
                {
                    case BehaviorPattern.EnemyActionType.Rest:
                        yield return StartCoroutine(Rest(pattern.duration));
                        break;

                    case BehaviorPattern.EnemyActionType.ChasePlayer:
                        yield return StartCoroutine(ChasePlayer(pattern.duration));
                        break;

                    case BehaviorPattern.EnemyActionType.RushToPlayer:
                        yield return StartCoroutine(RushToPlayer(pattern.duration));
                        break;

                    case BehaviorPattern.EnemyActionType.ShootAtPlayer:
                        yield return StartCoroutine(ShootAtPlayer(pattern.duration));
                        break;

                    // 추가된 행동 패턴에 대한 처리 추가

                    // 기다린 후에 다음 패턴으로 진행
                    //yield return null;
                }
            }
        }
    }


    //패턴s
    IEnumerator Rest(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            //Debug.Log("Rest...");
            yield return null;
            timer += Time.deltaTime;
        }
    }

    IEnumerator RushToPlayer(float duration)
    {
        float timer = 0f;

        Vector3 chaseDir = ( player.transform.position - gameObject.transform.position).normalized;


        while (timer < duration)
        {
            this.gameObject.transform.position = 

            transform.position += chaseDir * moveSpeed * Time.deltaTime;

            //Debug.Log("Chasing..");
            yield return null;
            timer += Time.deltaTime;
        }
    }

    IEnumerator ChasePlayer(float duration)
    {
        float timer = 0f;


        while (timer < duration)
        {
            this.gameObject.transform.position = 

            transform.position += ( player.transform.position - gameObject.transform.position).normalized * moveSpeed * Time.deltaTime;

            //Debug.Log("Chasing..");
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
        }
    }

    IEnumerator ShootAtPlayer(float duration)       //이러면 달리면서 중간에 한번 쏘기 가능?
    {
        float timer = 0f;

        Debug.Log("Shoot");

        while (timer < duration)
        {
            Debug.Log("shoot and waing..");
            yield return null;
            timer += Time.deltaTime;
        }
    }

    


    #endregion
}
