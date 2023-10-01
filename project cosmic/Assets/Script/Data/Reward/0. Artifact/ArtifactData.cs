using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewArtifactData", menuName = "ArtifactData")]
public class ArtifactData : ScriptableObject
{
    //
    public int artifactID = 0;
    public string artifactName = "Artifact Name";

    public EffectNode[] effectText; 


    public string commentText = "Comment";
    public Sprite artifactSprite;



    public bool isAttackReward = false;//공격 바뀜?
    public int attackID = 0;

    public bool isActionReward = false;//얻는 액션 있음?
    public int actionID = 0;

    public bool isCompanionReward = false;    //얻는 추종자 있음?
    public int companionID = 0;



    //Hp
    public int maxHPOffset = 0;
    public int hPOffset = 0;

    //Mentality
    public int mentalOffset = 0;

    //Move speed
    public float speedOffset = 0;

    //status
    public int physicalOffset = 0;
    public int willPowerOffset = 0;
    public int knowledgeOffset = 0;
    public int charmOffset = 0;


    //system    액션스택, 최대 액션스택
    public int actionStackOffset = 0;
    public int actionLimitOffset = 0;


    //coin
    public int coinOffset = 0;

    //
    public int cameraSizeOffset = 0;

}

[System.Serializable]
public class EffectNode
{
    bool reward;
    string text;
}
