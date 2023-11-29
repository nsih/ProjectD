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
public class ResultsData
{
    public string testResultName;
    [TextArea(2, 5)]
    public string resultText;

    public Sprite resultSprite;

    public EventOffset[] eventOffset;
}

[System.Serializable]
public class EventOffset
{
    public OffsetType offsetType;

    public float offset;
}

public enum TestType  //~~test!
{
    physical,
    willPower,
    knowledge,
    charm
}

[System.Serializable]
public enum OffsetType
{
    // HP
    MaxHPOffset,
    HpFullHeal,

    // Sanity
    MaxSanityOffset,
    SanityOffset,

    // Damage
    PlusPlayerDamageOffset,
    MultiplyPlayerDamageOffset,

    // Attack delay (speed)
    AttackDelay,

    // Move speed
    SpeedOffset,

    // Status
    PhysicalOffset,
    WillPowerOffset,
    KnowledgeOffset,
    CharmOffset,
    RandomStatOffset,

    // System (Action Stack, Maximum Action Stack)
    ActionStackOffset,
    ActionLimitOffset,

    // Coin
    CoinOffset,

    // Vision
    CameraSizeOffset,

    // Reward
    ArtifactID,
    ActionID,
    FuncID
}

