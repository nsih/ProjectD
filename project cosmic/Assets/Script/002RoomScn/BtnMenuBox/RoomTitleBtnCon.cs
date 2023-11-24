using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
public class RoomTitleBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    GameObject gameManager;
    GameObject roomUICanvas;
    GameObject pnlBackGround;
    GameObject doubleCheckPopup;
    GameObject doubleCheckText;
    Button btnYes;
    Button btnNo;


    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");
        doubleCheckPopup = pnlBackGround.transform.Find("DoubleCheckPopup").gameObject;

        doubleCheckText = doubleCheckPopup.transform.GetChild(0).gameObject;
        btnYes = doubleCheckPopup.transform.GetChild(1).GetComponent<Button>();
        btnNo = doubleCheckPopup.transform.GetChild(2).GetComponent<Button>();

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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            gameManager.GetComponent<SFXManager>().PlaySound(SfxType.BtnClick);
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }



    public void OnclickRoomTitle()
    {
        //gameManager.GetComponent<SFXManager>().PlaySound(SfxType.BtnClick);
        
        if(!GameManager.isLoading && !RoomDialogueManager.isRoomTalking)
        {
            OpenPopup();

            doubleCheckText.GetComponent<TextMeshProUGUI>().text = "피곤한데..";

            btnYes.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "뭔 방송이여..";
            btnNo.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "할 건 해야지";
        }
    }



    ///////////////// YES or NO
    void OnClickYes()
    {
        ClosePopup();

        SceneManager.LoadScene("ScnTitle");
    }

    void OnClickNo()
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
