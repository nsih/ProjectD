using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChitchatBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject roomUICanvas;


    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");


        normalColor = GetComponent<Image>().color;
        float r = Mathf.Clamp(normalColor.r - 0.2f, 0f, 1f);
        float g = Mathf.Clamp(normalColor.g - 0.2f, 0f, 1f);
        float b = Mathf.Clamp(normalColor.b - 0.2f, 0f, 1f);
        hoverColor = new Color(r, g, b, normalColor.a);        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }



    public void OnClickChitchat()
    {
        RoomDialogueCon.roomFlag = 0;   //잡담중 랜덤 플레그
        roomUICanvas.GetComponent<RoomDialogueCon>().currentIndex = 0;
        roomUICanvas.GetComponent<RoomDialogueCon>().StartDialogue();
    }

    /*
    private Color normalColor;      // 버튼의 기본 색상
    private Color hoverColor;       // 마우스를 올렸을 때의 색상

    private void Start()
    {
        // 버튼의 기본 색상을 저장합니다.
        normalColor = GetComponent<Image>().color;

        // RGB 값을 각각 10씩 올린 hoverColor를 생성합니다.
        float r = Mathf.Clamp(normalColor.r + 0.1f, 0f, 1f);
        float g = Mathf.Clamp(normalColor.g + 0.1f, 0f, 1f);
        float b = Mathf.Clamp(normalColor.b + 0.1f, 0f, 1f);
        hoverColor = new Color(r, g, b, normalColor.a);
    }

    // 마우스를 버튼 위로 올릴 때 호출되는 콜백 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼의 색상을 변경합니다.
        GetComponent<Image>().color = hoverColor;
    }

    // 마우스를 버튼에서 내릴 때 호출되는 콜백 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        // 버튼의 색상을 기본 색상으로 되돌립니다.
        GetComponent<Image>().color = normalColor;
    }
    */
}
