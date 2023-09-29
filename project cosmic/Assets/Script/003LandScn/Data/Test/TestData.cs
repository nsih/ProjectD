using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTestData", menuName = "TestData")]
public class TestData : ScriptableObject
{
    public int testID;

    public string testName;
    [TextArea(3, 10)]
    public string testText;
    public Sprite testSprite;

    public TestType testType;
    public int testOffset;

    public bool isTest = true;



    public ResultsData[] results;
}

[System.Serializable]
public class ResultsData
{
    public string testResultName;
    [TextArea(2, 5)]
    public string resultText;

    public Sprite resultSprite;


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

    public int actionID;

    //
    public int funcID;
}


public enum TestType  //~~test!
{
    physical,
    willPower,
    knowledge,
    charm
}

