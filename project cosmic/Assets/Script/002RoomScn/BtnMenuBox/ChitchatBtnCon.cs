using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChitchatBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject roomUICanvas;
    GameObject pnlBackGround;


    GameObject dialogueManager;


    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");

        dialogueManager = GameObject.Find("DialogueManager");


        normalColor = GetComponent<Image>().color;
        float r = Mathf.Clamp(normalColor.r - 0.2f, 0f, 1f);
        float g = Mathf.Clamp(normalColor.g - 0.2f, 0f, 1f);
        float b = Mathf.Clamp(normalColor.b - 0.2f, 0f, 1f);
        hoverColor = new Color(r, g, b, normalColor.a);     
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //gameManager.GetComponent<SFXManager>().PlaySFX();
        GetComponent<Image>().color = hoverColor;
        //Debug.Log(hoverColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }



    public void OnClickChitchat()
    {        
        if(!RoomDialogueManager.isRoomTalking)
        {
            dialogueManager.GetComponent<RoomDialogueManager>().ChangeDialogue("chitchat");
            dialogueManager.GetComponent<RoomDialogueManager>().StartDialogue();

            
        }
    }
}
