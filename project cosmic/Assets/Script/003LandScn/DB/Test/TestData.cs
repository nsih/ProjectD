using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "TestData")]
public class TestData : ScriptableObject
{
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
    public int rPhysicalStat;
    public int rWillPowerStat;
    public int rKnowledgeStat;
    public int rCharmStat;


    //system
    public int rDoomCount;

    public int rActionStack;
    public int rRecover;
    

}


public enum TestType  //~~test!
{
    physical,
    willPower,
    knowledge,
    charm
}

