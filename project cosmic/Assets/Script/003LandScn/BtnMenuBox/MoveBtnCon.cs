using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MoveBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject landUiCanvas;


    GameObject ButtonText;
    GameObject ButtonImage;


    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");

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



    public void OnClickMove()
    {
        if(GameManager.doomCount != 0)
        {
            landUiCanvas.GetComponent<LandUICon>().ShowMiniMap();
        }

        else
        {
            Debug.Log("씬 이동 했다 칩시다.");
            //SceneManager.LoadScene("ScnLand");
        }
        
    }


    void ShowButtonImage()
    {
        if(GameManager.isActionPhase)
        {
            if(GameManager.doomCount >0)
            {
                ButtonImage.GetComponent<Image>().color = Color.white;
                ButtonText.GetComponent<TextMeshProUGUI>().text = "이동";
            }

            else
            {
                ButtonImage.GetComponent<Image>().color = Color.white;
                ButtonText.GetComponent<TextMeshProUGUI>().text = "<color=##8258FA>운명</color>";
            }
        }
        else
        {
            ButtonImage.GetComponent<Image>().color = Color.clear;
            ButtonText.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>이동</color>";
        }



    }

    //textComponent.text = "Action Phase ( <color=#00e5ff>" + GameManager.actionStack + "</color> )";
}
