using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyGameObject;

    public Animator enemyAnimator;


    public string mobName;

    public float hp;
    public int damage;
    public float moveSpeed;


    public float attackDelay;
    public float bulletSpeed;

    public bool knockBackable;


    public List<BehaviorPattern> behaviorPatterns = new List<BehaviorPattern>();
}



[Serializable]
public class BehaviorPattern
{
    public enum EnemyActionType
    {
        Rest,
        ChasePlayer,
        RushToPlayer,
        ShootAtPlayer,
        MoveShootPlayer,
        TanmakCircle
    }

    public EnemyActionType actionType;
    public float duration;
    public int cycles;

    public int bulletCount;
}