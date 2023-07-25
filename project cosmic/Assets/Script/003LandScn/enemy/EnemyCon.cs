using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    GameObject gameManager;

    GameObject player;
    GameObject playerHit;
    GameObject playerStrike;


    public EnemyData enemyData;

    CircleCollider2D circleCollider;


    Animator enemyAnimator;

    public int mobNum;

    public int hp;
    public int damage;
    public float moveSpeed;
    public float attackDelay;
    public float bulletSpeed;
    public MovingType movingType;
    public AttackType attackType;
    public bool knockBackable;




    bool isKnockBack;
    float knockBackDuration = 0.05f;
    float knockBackSpeed = 15f;
    



    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        player = GameObject.Find("player");
        playerHit = GameObject.Find("playerHit");
        playerStrike = GameObject.Find("StrikePivot").transform.GetChild(0).gameObject;

        mobNum = enemyData.mobNum;
        enemyAnimator = enemyData.enemyAnimator;


        isKnockBack = false;
    }

    void Update ()
    {

    }

    void FixedUpdate() 
    {
        Moving();
    }

    void OnEnable() 
    {
        InitializeEnemyStatus();
    }

    void OnDisable() 
    {
        if(gameManager.GetComponent<BattleEventManager>().isPoolAllDone() == true)
        {
            gameManager.GetComponent<StageManager>().EndRoomEventPhase(StageManager.map[ GameManager.currentRoom ].roomType);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider == playerHit.GetComponent<Collider2D>() )
        {
            Debug.Log("asd");
            HandlePlayerStrike();
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other == playerStrike.GetComponent<Collider2D>() )
        {
            HandlePlayerStrike();
            player.GetComponent<PlayerCon>().StrikeCon();
        }
    }


    ///

    void InitializeEnemyStatus()
    {
        hp = enemyData.hp;
        damage = enemyData.damage;
        moveSpeed = enemyData.moveSpeed;

        attackDelay = enemyData.attackDelay;
        bulletSpeed = enemyData.bulletSpeed;

        movingType = enemyData.movingType;
        attackType = enemyData.attackType;  

        knockBackable = enemyData.knockBackable;
    }


    void Moving() 
    {
        if(movingType == MovingType.none)
        {

        }
        else if(movingType == MovingType.chase)
        {

        }
        else if(movingType == MovingType.rush)
        {

        }
        else if(movingType == MovingType.run)
        {

        }
    }

    void Attack()
    {
        if(attackType == AttackType.none)
        {

        }

        else if(attackType == AttackType.aimShot)
        {

        }
    }




    ////
    void HandlePlayerStrike()   //공격받았을때
    {
        KnockBack();
        if( (hp - PlayerInfo.attackDamage) > 0)
        {
            hp = hp - PlayerInfo.attackDamage;
        }
        
        else
        {
            hp = 0;
            this.gameObject.SetActive(false);
        }
    }

    void KnockBack()
    {
        if (knockBackable && !isKnockBack)
        {
            isKnockBack = true;
            Vector3 knockBackDirection = -(player.transform.position - transform.position).normalized; // 현재 오브젝트가 보는 방향의 반대 방향으로 밀리도록 설정
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
}
