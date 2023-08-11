using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ArtifactBtnCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject gameManager;
    GameObject landUiCanvas;
    GameObject pnlBackGround;
    GameObject artifactPopup;


    private Color normalColor;
    private Color hoverColor;


    Button btnClose;


    GameObject ButtonText;
    GameObject ButtonImage;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");

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


    public void OnClickArtifact()
    {
        if(!GameManager.isLoading && !landUiCanvas.GetComponent<LandUICon>().isRoomIntroPanel() )
        {
            OpenPopup();
        }
    }
    


    void ShowButtonImage()
    {
        ButtonImage.GetComponent<Image>().color = Color.white;
        ButtonText.GetComponent<TextMeshProUGUI>().text = "아티팩트";
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
