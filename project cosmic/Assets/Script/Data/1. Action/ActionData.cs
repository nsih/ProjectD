using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewActionData", menuName = "ActionData")]
public class ActionData : ScriptableObject
{
    public int actionID;

    public string name;
    public int cost;
    public string actionText;
    public Sprite sprite;

    public TestType testType;
    public string testTypeS;


    public int testOffset;

    public ResultsData[] results;

    //얻을 행동 코멘트
    [TextArea(3, 10)]
    public string beforeComment = "Before Comment";

    //얻은 행동 코멘트
    [TextArea(3, 10)]
    public string afterComment = "After Comment";
}