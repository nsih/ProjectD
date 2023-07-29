using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewArtifactData", menuName = "ArtifactData")]
public class ArtifactData : ScriptableObject
{
    public int artifactID;
    public string artifactName;
    public Sprite artifactSprite;



    //Hp
    public int maxHPOffset;
    public int hPOffset;


    //damage
    public int plusPlayerDamageOffset;
    public float multiplyPlayerDamageOffset;

    //speed
    public float speedOffset;

    //status
    public int physicalOffset;
    public int willPowerOffset;
    public int knowledgeOffset;
    public int charmOffset;


    //system
    public int actionStackOffset;
    public int actionLimitOffset;  //매턴 얻는 스택


    //얻는 무기가 있다면
    public int weaponID;


    public int coinMod;
}
