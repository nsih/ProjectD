using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewArtifactData", menuName = "ArtifactData")]
public class ArtifactData : ScriptableObject
{
    //
    public int artifactID;
    public string artifactName;
    public Sprite artifactSprite;



    //Hp
    public int maxHPOffset;
    public int hPOffset;


    //attack damage
    public int plusPlayerDamageOffset;
    public float multiplyPlayerDamageOffset;

    //attack delay (speed)

    //Move speed
    public float speedOffset;

    //status
    public int physicalOffset;
    public int willPowerOffset;
    public int knowledgeOffset;
    public int charmOffset;


    //system    액션스택, 최대 액션스택
    public int actionStackOffset;
    public int actionLimitOffset;


    //얻는 무기가 있다면
    public int weaponID;

    //coin
    public int coinMod;
}
