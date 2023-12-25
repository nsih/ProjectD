using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewAlterData", menuName = "AlterData")]
public class AlterData : ScriptableObject
{
    public int alterID;

    public string alternName;
    [TextArea(3, 10)]
    public string alterText;
    [TextArea(3, 10)]
    public string afterWord;

    public Sprite alterSprite;

    public OutcomeOffset[] outcomeOffset;

    public bool isUsed;
}