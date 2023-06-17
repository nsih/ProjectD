using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//옵션창 여는 버튼
public class BtnOptionController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject optionView;

    private void Start() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        optionView = pnlBackGround.transform.Find("OptionView").gameObject;
    }
    public void EnableClick()
    {
        optionView.SetActive(true);
    }
    public void DisableClick()
    {
        optionView.SetActive(false);
    }
}
