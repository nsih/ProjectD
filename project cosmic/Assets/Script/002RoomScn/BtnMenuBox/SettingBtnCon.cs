using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class SettingBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    GameObject gameManager;
    GameObject roomUICanvas;
    GameObject pnlBackGround;
    GameObject settingPopup;

    Button btnClose;

    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");
        settingPopup = pnlBackGround.transform.Find("SettingPopup").gameObject;
        
        btnClose = settingPopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();

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


    public void OnclickSetting()
    {
        //gameManager.GetComponent<SFXManager>().PlaySound(SfxType.BtnClick);

        if(!GameManager.isLoading && !RoomDialogueManager.isRoomTalking)
        {
            OpenPopup();
        }
    }

    /////////////// f
    void OpenPopup()
    {
        settingPopup.SetActive(true);
        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        settingPopup.SetActive(false);
        btnClose.onClick.RemoveAllListeners();
    }
}
