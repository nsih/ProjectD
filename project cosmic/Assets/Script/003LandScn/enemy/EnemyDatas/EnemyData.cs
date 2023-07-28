using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyGameObject;

    public Animator enemyAnimator;


    public int mobNum;

    public float hp;
    public int damage;
    public float moveSpeed;


    public float attackDelay;
    public float bulletSpeed;

    public bool knockBackable;


    public MovingType movingType;
    public AttackType attackType;
    public GameObject AttackBullet;
}



public enum MovingType
{
    none,
    chase,          //범위 안에 들어오면 쫒기
    rush,           //범위 안에 들어오면 돌진
    run,            //도망
}

public enum AttackType  //몸통 박치기는 됐고. 걍 탄막만
{
    none,
    aimShot,        //한발
}
