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
    GameObject artifactPopup;


    Button btnClose;


    GameObject ButtonText;
    GameObject ButtonImage;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");
        artifactPopup = pnlBackGround.transform.Find("ArtifactPopup").gameObject;
        btnClose = artifactPopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();

        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update ()
    {
        ShowButtonImage();
    }


    public void OnClickArtifact()
    {
        OpenPopup();
    }


    void ShowButtonImage()
    {
        ButtonImage.GetComponent<Image>().color = Color.white;
        ButtonText.GetComponent<TextMeshProUGUI>().text = "유물";
    }

    void OpenPopup()
    {
        artifactPopup.SetActive(true);

        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        artifactPopup.SetActive(false);

        btnClose.onClick.RemoveAllListeners();
    }
}
