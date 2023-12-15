using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTestData", menuName = "TestData")]
public class TestEventData : ScriptableObject
{
    public int testID;

    public bool isTested;

    public string testName;
    [TextArea(3, 10)]
    public string testText;
    public Sprite testSprite;

    public TestType testType;

    public string testTypeS;

    public int testOffset;


    public ResultsData[] results;
}

[System.Serializable]
//test only
public class ResultsData
{
    public string testResultName;
    [TextArea(2, 5)]
    public string resultText;

    public Sprite resultSprite;

    public OutcomeOffset[] eventOffset;
}


////////////////////////////////////

[System.Serializable]

//수치
public class OutcomeOffset
{
    public OutcomeOffsetType offsetType;

    public float offset;
}

public enum TestType  //~~test!
{
    Physical,
    Mental,
    Charm,
    Random,
    None
}

[System.Serializable]
public enum OutcomeOffsetType
{
    // Status
    PhysicalOffset,
    MentalOffset,
    CharmOffset,
    RandomStatOffset,


    // HP
    MaxHPOffset,
    HpOffset,

    //AP
    APOffset,
    MaxAPOffset,


    // Coin
    CoinOffset,

    // Damage
    PlusPlayerDamageOffset,
    MultiplyPlayerDamageOffset,

    // Attack delay (speed)
    AttackDelay,

    // Move speed
    MoveSpeedOffset,

    // Vision
    CameraSizeOffset,

    // Reward etc
    ArtifactID,
    ActionID,
    FuncID,
    AttackID,
    CompanionID,
}

