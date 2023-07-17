using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RecoverBtnCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject pnlBackGround;
    GameObject doubleCheckPopup;

    GameObject doubleCheckText;

    GameObject ButtonImage;

    Button btnYes;
    Button btnNo;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        pnlBackGround = GameObject.Find("PnlBackGround");
        doubleCheckPopup = pnlBackGround.transform.Find("DoubleCheckPopup").gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;

        doubleCheckText = doubleCheckPopup.transform.GetChild(0).gameObject;
        btnYes = doubleCheckPopup.transform.GetChild(1).GetComponent<Button>();
        btnNo = doubleCheckPopup.transform.GetChild(2).GetComponent<Button>();
    }

    public void Update ()
    {
        ShowButtonImage();
    }

    //
    public void OnclickRecover()
    {
        if(GameManager.isActionPhase && GameManager.actionStack != 0)
        {
            OpenPopup();
        
            doubleCheckText.GetComponent<TextMeshProUGUI>().text = "회복 하시겠습니까? (action -1)";
        }
    }

    void ShowButtonImage()
    {
        if(GameManager.isActionPhase && GameManager.actionStack != 0)
        {
            ButtonImage.GetComponent<Image>().color = Color.white;
        }
        else
        {
            ButtonImage.GetComponent<Image>().color = Color.clear;
        }
    }


    /////////////////
    public void OnClickYes()
    {
        gameManager.GetComponent<GameManager>().ActionStackModify(-1);

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
