using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChitchatBtnCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject roomUICanvas;


    int chitchatFlag;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");

        chitchatFlag = 0;
    }



    public void OnClickChitchat()
    {
        RoomDialogueCon.roomFlag = 0;   //잡담중 랜덤 플레그
        roomUICanvas.GetComponent<RoomDialogueCon>().currentIndex = 0;
        roomUICanvas.GetComponent<RoomDialogueCon>().StartDialogue();
    }
}
