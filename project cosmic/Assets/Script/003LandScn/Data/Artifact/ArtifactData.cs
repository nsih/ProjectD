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

    public bool isCompanion;    //얻는 추종자 있음?
    public int companionID;



    //Hp
    public int maxHPOffset;

    public bool hpFullHeal;
    public int hPOffset;

    //Mentality
    public int mentalOffset;

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
    public int coinOffset;

    //
    public int cameraSizeOffset;

}
