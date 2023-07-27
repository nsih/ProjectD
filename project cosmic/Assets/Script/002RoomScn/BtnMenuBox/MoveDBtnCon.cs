using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MoveDBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject roomUICanvas;

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
        doubleCheckPopup = roomUICanvas.transform.Find("DoubleCheckPopup").gameObject;

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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
    }

    public void OnclickMovingD()
    {
        if(true)// !isTalking
        {
            OpenPopup();
        
            doubleCheckText.GetComponent<TextMeshProUGUI>().text = "지금 드림랜드로 이동할거야?";

            btnYes.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "그래";
            btnNo.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "아직";
        }
    }


    ///////////////// YES or NO
    void OnClickYes()
    {
        ClosePopup();

        SceneManager.LoadScene("ScnLand");

        //스테이지 0 초기화 ㄱ

        
        gameManager.GetComponent<GameManager>().OpenNewStage();

        gameManager.GetComponent<StageManager>().StartRoomEventPhase(RoomType.Null);
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
