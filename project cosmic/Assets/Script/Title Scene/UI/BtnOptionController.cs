using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnOptionController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject optionView;

    private void Awake() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        optionView = pnlBackGround.transform.Find("OptionView").gameObject;
    }
    public void OnClick()
    {
        optionView.SetActive(true);
    }
}
