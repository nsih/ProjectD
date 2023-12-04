using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class RecoverBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject roomUiCanvas;
    GameObject doubleCheckPopup;

    GameObject doubleCheckText;


    GameObject ButtonText;
    GameObject ButtonImage;

    Button btnYes;
    Button btnNo;

    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUiCanvas = GameObject.Find("RoomUICanvas");
        doubleCheckPopup = roomUiCanvas.transform.Find("DoubleCheckPopup").gameObject;

        normalColor = GetComponent<Image>().color;
        float r = Mathf.Clamp(normalColor.r - 0.2f, 0f, 1f);
        float g = Mathf.Clamp(normalColor.g - 0.2f, 0f, 1f);
        float b = Mathf.Clamp(normalColor.b - 0.2f, 0f, 1f);
        hoverColor = new Color(r, g, b, normalColor.a);

        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;

        doubleCheckText = doubleCheckPopup.transform.GetChild(0).gameObject;
        btnYes = doubleCheckPopup.transform.GetChild(1).GetComponent<Button>();
        btnNo = doubleCheckPopup.transform.GetChild(2).GetComponent<Button>();
    }

    public void Update ()
    {
        ShowButtonImage();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }

    //
    public void OnclickRecover()
    {
        if(GameManager.isEventEnd && PlayerInfo.ap != 0)
        {
            OpenPopup();
        
            doubleCheckText.GetComponent<TextMeshProUGUI>().text = "회복 하시겠습니까? (action -1)";
        }
    }

    void ShowButtonImage()
    {
        if(GameManager.isEventEnd && PlayerInfo.ap != 0)
        {
            ButtonImage.GetComponent<Image>().color = Color.white;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "메뉴";
        }
        else
        {
            ButtonImage.GetComponent<Image>().color = Color.clear;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>메뉴</color>";
        }
    }


    /////////////////
    public void OnClickYes()
    {
        gameManager.GetComponent<PlayerInfo>().APModify(-1);

        gameManager.GetComponent<PlayerInfo>().HpModify(1);

        ClosePopup();
    }

    public void OnClickNo()
    {
        ClosePopup();
    }





    ///////////////
    void OpenPopup()
    {
        doubleCheckPopup.SetActive(true);

        btnYes.onClick.AddListener(OnClickYes);
        btnNo.onClick.AddListener(OnClickNo);
    }

    void ClosePopup()
    {
        doubleCheckPopup.SetActive(false);

        btnYes.onClick.RemoveAllListeners();
        btnNo.onClick.RemoveAllListeners();
    }
}
