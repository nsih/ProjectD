using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChitchatBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
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
        GetComponent<Image>().color = hoverColor;

        gameManager.GetComponent<SFXManager>().PlaySound(SfxType.BtnHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            gameManager.GetComponent<SFXManager>().PlaySound(SfxType.BtnClick);
        }
    }




    public void OnClickChitchat()
    {
        //gameManager.GetComponent<SFXManager>().PlaySound(SfxType.BtnClick);

        if(!GameManager.isLoading && !RoomDialogueManager.isRoomTalking)
        {
            dialogueManager.GetComponent<RoomDialogueManager>().ChangeDialogue("chitchat");
            dialogueManager.GetComponent<RoomDialogueManager>().StartDialogue();
        }
    }
}
