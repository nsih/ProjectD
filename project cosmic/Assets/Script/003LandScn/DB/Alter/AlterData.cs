using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAlterData", menuName = "AlterData")]
public class AlterData : ScriptableObject
{
    public int alterID;

    public string alternName;
    [TextArea(3, 10)]
    public string actionText;

    public AlterResultsData result;
}


[System.Serializable]
public class AlterResultsData
{
    public string Text;


    //Hp
    public int maxHPOffset;

    public bool hpFullHeal;
    public int hPOffset;

    //Mentality
    public int mentalOffset;


    //attack damage
    public int plusPlayerDamageOffset;
    public float multiplyPlayerDamageOffset;

    //attack delay (speed)

    public float attackDelay;

    //Move speed
    public float speedOffset;

    //status
    public int physicalOffset;
    public int willPowerOffset;
    public int knowledgeOffset;
    public int charmOffset;

    public int randomOffset;


    //system    액션스택, 최대 액션스택
    public int actionStackOffset;
    public int actionLimitOffset;


    //coin
    public int coinOffset;

    //
    public int cameraSizeOffset;

    //
    public int artifactID;
    public int funcID;
}