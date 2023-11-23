using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
public class LeaveRoomBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }

    public void OnclickLeaveRoom()
    {
        if(!GameManager.isLoading)
        {
            if(!RoomDialogueManager.isRoomTalking)// !isTalking
            {
                OpenPopup();
            
                doubleCheckText.GetComponent<TextMeshProUGUI>().text = "지금 방을 떠날까?";

                btnYes.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "나가자";
                btnNo.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "좀 더 밍기적 거릴레..";
            }
        }
    }


    ///////////////// YES or NO
    void OnClickYes()
    {
        ClosePopup();

        //스테이지 0 초기화 및 시작
        SceneManager.LoadSceneAsync("ScnLand").completed += OnSceneLoaded;
    }

    void OnSceneLoaded(AsyncOperation operation) {
    if (operation.isDone) {
        // ScnLand 씬 로드 후 OpenNewStage 함수를 호출
        gameManager.GetComponent<GameManager>().OpenNewStage();
    }
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
