using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerAttackData", menuName = "PlayerAttackData")]
public class PlayerAttackData : ScriptableObject
{
    //
    public int attackID;
    public Sprite bulletSprite;

    public int spritePrior;



    //공격 데미지
    public int plusDMG;

    public float multiflyDMG;   //0.1 ~ n


    //공격 속도
    public float plusattackSpeed;   //
    public float multiflyattackSpeed;   //


    //총알 줄기
    public int plusBulletBranch;
    public int multiflyBulletBranch;


    //
    public int bulletFunctionID;

}
