using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ArtifactBtnCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject landUiCanvas;
    GameObject pnlBackGround;
    GameObject ritualPopup;


    Button btnClose;


    GameObject ButtonText;
    GameObject ButtonImage;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");
        ritualPopup = pnlBackGround.transform.Find("RitualPopup").gameObject;
        btnClose = ritualPopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();

        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update ()
    {
        ShowButtonImage();
    }


    public void OnClickRitual()
    {
        OpenPopup();
    }


    void ShowButtonImage()
    {
        if(GameManager.isActionPhase && GameManager.actionStack > 0)
        {
            ButtonImage.GetComponent<Image>().color = Color.white;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "의식";
        }
        else
        {
            ButtonImage.GetComponent<Image>().color = Color.clear;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>의식</color>";
        }
    }

    void OpenPopup()
    {
        ritualPopup.SetActive(true);

        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        ritualPopup.SetActive(false);

        btnClose.onClick.RemoveAllListeners();
    }
}
