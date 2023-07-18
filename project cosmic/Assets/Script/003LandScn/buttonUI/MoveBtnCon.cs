using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveBtnCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject landUiCanvas;


    GameObject ButtonText;
    GameObject ButtonImage;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");

        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update ()
    {
        ShowButtonImage();
    }


    public void OnClickMove()
    {
        landUiCanvas.GetComponent<LandUICon>().ShowMiniMap();
    }


    void ShowButtonImage()
    {
        if(GameManager.isActionPhase)
        {
            ButtonImage.GetComponent<Image>().color = Color.white;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "이동";
        }
        else
        {
            ButtonImage.GetComponent<Image>().color = Color.clear;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>이동</color>";
        }
    }

    //textComponent.text = "Action Phase ( <color=#00e5ff>" + GameManager.actionStack + "</color> )";
}
