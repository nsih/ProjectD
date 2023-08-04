using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewActionData", menuName = "ActionData")]
public class ActionData : ScriptableObject
{
    public int actionID;

    public string actionName;
    [TextArea(3, 10)]
    public string actionText;
    public Sprite artifactSprite;

    public TestType actionTestType;
    public int testOffset;

    public ResultsData[] results;
}