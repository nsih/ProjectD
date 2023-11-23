using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class RecordBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject roomUICanvas;
    GameObject recordPopup;

    Button btnClose;

    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");
        recordPopup = roomUICanvas.transform.Find("RecordPopup").gameObject;
        
        btnClose = recordPopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();

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

    public void OnclickRecord()
    {
        if(GameManager.isLoading)
        {
            OpenPopup();
        }
    }

    /////////////// f
    void OpenPopup()
    {
        recordPopup.SetActive(true);
        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        recordPopup.SetActive(false);
        btnClose.onClick.RemoveAllListeners();
    }
}
