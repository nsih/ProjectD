using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewArtifactData", menuName = "ArtifactData")]
public class ArtifactData : ScriptableObject
{
    //
    public int artifactID;
    public string artifactName;
    public string benefitText;
    public string penaltyText;
    public string commentText;
    public Sprite artifactSprite;



    ///////
    public bool isWeaponChange;//무기 바뀜?
    public int weaponID;

    public bool isActionReword;//얻는 액션 있음?
    public int actionID;



    //Hp
    public int maxHPOffset;
    public int hPOffset;

    //Mentality
    public int mentalOffset;


    //attack damage
    public int plusPlayerDamageOffset;
    public float multiplyPlayerDamageOffset;

    //attack delay (speed)

    public int attackDelay;

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


    //coin
    public int coinMod;

}
