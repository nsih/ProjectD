using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewActionData", menuName = "ActionData")]
public class ActionData : ScriptableObject
{
    public int actionID;
    public rewardRate rewardRate = rewardRate.C;
    
    public Sprite icon;

    public string actionName = "Action Name";
    public int cost = 1;

    [TextArea(2, 15)]
    public string actionText = "Text";

    public TestType testType;
    public string testTypeS;


    public int testOffset = 0;


    public ActionResultsData[] results;



    public bool isUsed;

    //얻을 행동 코멘트
    [TextArea(3, 10)]
    public string beforeComment = "Before Comment";

    //얻은 행동 코멘트
    [TextArea(3, 10)]
    public string afterComment = "After Comment";
}


[System.Serializable]
//test only
public class ActionResultsData
{
    public OutcomeOffset[] eventOffset;
}