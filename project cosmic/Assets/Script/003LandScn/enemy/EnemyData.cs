using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public int enemyNum;
    public string enemyName;
    public int health;
    public int attackDamage;
    public float speed;
    public Sprite enemySprite;
    public EnemyBehaviorType enemyAttackType;
}



public enum EnemyBehaviorType
{
    tackle,             //돌진
    basicBullet,        //조준 1 발사
    multipleBullet,     //조준 5 발사
}
