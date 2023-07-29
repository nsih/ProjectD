using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTestData", menuName = "TestData")]
public class TestData : ScriptableObject
{
    public int testID;
    public string testName;

    public Sprite testSprite;

    [TextArea(3, 10)]
    public string testText;

    public TestType testType;

    int collectionValue;



    public ResultsData[] results;
}

[System.Serializable]
public class ResultsData
{
    public string resultName;
    [TextArea(2, 5)]
    public string resultText;

    public Sprite resultSprite;
    
    public int rArtifactID;
    public int rRitualID;






    //status
    public int physicalOffset;
    public int willPowerOffset;
    public int knowledgeOffset;
    public int charmOffset;


    //system
    public int doomCountOffset;
    public int actionStackOffset;
    public int maxActionStackOffset;

    public int maxHpOffset;
    public int hpOffset;

    public int coinOffset;
}


public enum TestType  //~~test!
{
    physical,
    willPower,
    knowledge,
    charm
}

