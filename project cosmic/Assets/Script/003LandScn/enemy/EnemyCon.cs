using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject player;


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
    



    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");

        mobNum = enemyData.mobNum;
        enemyAnimator = enemyData.enemyAnimator;
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
        if(other.collider == player.GetComponent<Collider2D>() )
        {
            this.gameObject.SetActive(false);
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
}
