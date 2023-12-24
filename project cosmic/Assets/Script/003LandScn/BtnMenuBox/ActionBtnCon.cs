using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ActionBtnCon : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    GameObject landUICanvas;

    GameObject ButtonText;
    GameObject ButtonImage;


    void Start()
    {
        landUICanvas = GameObject.Find("LandUICanvas");

        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;
    }

    public void OnClickAction()
    {
        landUICanvas.GetComponent<LandUICon>().ActionListSwitch();
    }
}

