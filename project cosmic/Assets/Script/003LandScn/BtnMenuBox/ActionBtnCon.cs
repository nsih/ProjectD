using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ActionBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject landUiCanvas;
    GameObject pnlBackGround;
    GameObject actionPopup;


    Button btnClose;


    GameObject ButtonText;
    GameObject ButtonImage;

    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");
        actionPopup = pnlBackGround.transform.Find("ActionPopup").gameObject;
        btnClose = actionPopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();

        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;

        normalColor = GetComponent<Image>().color;
        float r = Mathf.Clamp(normalColor.r - 0.2f, 0f, 1f);
        float g = Mathf.Clamp(normalColor.g - 0.2f, 0f, 1f);
        float b = Mathf.Clamp(normalColor.b - 0.2f, 0f, 1f);
        hoverColor = new Color(r, g, b, normalColor.a);
    }

    void Update ()
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


    public void OnClickAction()
    {
        OpenPopup();
    }


    void ShowButtonImage()
    {
        if(GameManager.isEventEnd && PlayerInfo.actionPoint > 0)
        {
            ButtonImage.GetComponent<Image>().color = Color.white;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "행동";
        }
        else
        {
            ButtonImage.GetComponent<Image>().color = Color.clear;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>행동</color>";
        }
    }

    void OpenPopup()
    {
        actionPopup.SetActive(true);

        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        actionPopup.SetActive(false);

        btnClose.onClick.RemoveAllListeners();
    }
}
