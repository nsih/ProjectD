using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionBtnCon : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject optionPopup;

    Button btnClose;


    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        optionPopup = pnlBackGround.transform.Find("OptionPopup").gameObject;

        btnClose = optionPopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();
    }

    public void OnclickOption()
    {
        OpenPopup();
    }


    //
    void OpenPopup()
    {
        optionPopup.SetActive(true);
        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        optionPopup.SetActive(false);
        btnClose.onClick.RemoveAllListeners();
    }
}
