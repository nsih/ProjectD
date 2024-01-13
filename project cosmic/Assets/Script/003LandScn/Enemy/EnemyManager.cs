using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        player = GameObject.Find("player");
        gameManager = GameObject.Find("GameManager");
        

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

    #region "충돌관련"
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !PlayerInfo.isInvincible)
        {
            Debug.Log(PlayerInfo.isInvincible);
            player.GetComponent<PlayerManager>().PlayerAttacked();
        }
    }

    //(Been attacked)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !PlayerInfo.isInvincible)
        {
            Debug.Log(PlayerInfo.isInvincible);
            player.GetComponent<PlayerManager>().PlayerAttacked();
        }

        if (other.gameObject.tag == "PlayerAttack")
        {
            //Debug.Log("HP : "+hp);
            HandlePlayerStrike(other.gameObject);

            other.gameObject.SetActive(false);
            //other.gameObject.GetComponent<PlayerBulletCon>().VanishOnCollision();
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
                    ///적 움직임 관련
                    case BehaviorPattern.EnemyActionType.Rest:
                        yield return StartCoroutine(Rest(pattern.duration));
                        break;

                    case BehaviorPattern.EnemyActionType.ChasePlayer:
                        yield return StartCoroutine(ChasePlayer(pattern.duration));
                        break;

                    case BehaviorPattern.EnemyActionType.RushToPlayer:
                        yield return StartCoroutine(RushToPlayer(pattern.duration));
                        break;


                    ///슈팅 관련
                    case BehaviorPattern.EnemyActionType.ShootAtPlayer:
                        yield return StartCoroutine(ShootPlayer(pattern.duration,pattern.cycles));
                        break;

                    case BehaviorPattern.EnemyActionType.MoveShootPlayer:
                        yield return StartCoroutine(MoveShootPlayer(pattern.duration,pattern.cycles));
                        break;

                    case BehaviorPattern.EnemyActionType.TanmakCircle:
                        yield return StartCoroutine(TanmakCircle(pattern.duration,pattern.cycles,pattern.bulletCount));
                        break;
                }
            }
        }
    }


    #region "움직임 관련 패턴"
    //암것도 안함
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

    //돌진
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

    //쫒기
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
    
    
    #endregion

    #region "슈팅 관련"

    //
    IEnumerator ShootPlayer(float duration, int cycles)
    {
        for ( int i = 0 ; i < cycles ; i++)
        {
            float timer = 0f;

            foreach (GameObject bullet in gameManager.GetComponent<EnemyBulletPoolManager>().enemyBulletPool)
            {
                if (!bullet.activeInHierarchy)
                {
                    //위치, 방향설정
                    bullet.transform.position = this.gameObject.transform.position;

                    Vector2 direction = (player.transform.position - bullet.transform.position).normalized;
                    bullet.transform.up = direction;

                    //활성화
                    bullet.SetActive(true);

                    bullet.GetComponent<EnemyBulletCon>().GetEnemyBulletMode(EnemyBulletType.NORMAL);


                    
                    break;
                }
            }

            while (timer < duration)
            {
                //Debug.Log("shoot and waing..");
                yield return null;
                timer += Time.deltaTime;
            }
        }
    }

    IEnumerator MoveShootPlayer(float duration, int cycles)
    {
        for ( int i = 0 ; i < cycles ; i++)
        {
            float timer = 0f;

            foreach (GameObject bullet in gameManager.GetComponent<EnemyBulletPoolManager>().enemyBulletPool)
            {
                if (!bullet.activeInHierarchy)
                {
                    //위치, 방향설정
                    bullet.transform.position = this.gameObject.transform.position;

                    Vector2 direction = (player.transform.position - bullet.transform.position).normalized;
                    bullet.transform.up = direction;

                    //활성화
                    bullet.SetActive(true);

                    bullet.GetComponent<EnemyBulletCon>().GetEnemyBulletMode(EnemyBulletType.NORMAL);


                    
                    break;
                }
            }

            while (timer < duration)
            {
                this.gameObject.transform.position = 

                transform.position += ( player.transform.position - gameObject.transform.position).normalized * moveSpeed * Time.deltaTime;

                timer += Time.deltaTime;

                yield return null;
            }
        }
    }

    
    //원형 탄막
    IEnumerator TanmakCircle(float duration, int cycles, int bulletCount)
    {
        for ( int i = 0 ; i < cycles ; i ++)
        {
            float timer = 0f;

            for(int j = 0 ; j < bulletCount ; j++)
            {
                foreach (GameObject bullet in gameManager.GetComponent<EnemyBulletPoolManager>().enemyBulletPool)
                {
                    if (!bullet.activeInHierarchy)
                    {
                        //position
                        bullet.transform.position = this.gameObject.transform.position;

                        // dir angle calc
                        float angle = j * (360f / bulletCount);
                        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                        bullet.transform.up = direction;

                        //활성화
                        bullet.SetActive(true);

                        bullet.GetComponent<EnemyBulletCon>().GetEnemyBulletMode(EnemyBulletType.NORMAL);

                        break;
                    }
                }
                
                while (timer < duration)
                {
                    Debug.Log("waiting..");
                    yield return null;
                    timer += Time.deltaTime;
                }
            }
        }
    }
    
    
    
    #endregion
    


    #endregion
}
