using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    //
    public int itemID = 0;
    public Sprite sprite;
    public string name = "Item Name";

    public OutcomeOffset[] outcomeOffset;


    //얻을 유물 코멘트
    [TextArea(2, 15)]
    public string beforeComment = "Before Comment";

    //얻은 유물 코멘트
    [TextArea(2, 10)]
    public string afterComment = "After Comment";

}
