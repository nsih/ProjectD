using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBtnCon : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject doubleCheckPopup;

    Button btnYes;
    Button btnNo;
    


    private void Start() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        doubleCheckPopup = pnlBackGround.transform.Find("DoubleCheckPopup").gameObject;

        btnYes = doubleCheckPopup.transform.GetChild(1).GetComponent<Button>();
        btnNo = doubleCheckPopup.transform.GetChild(2).GetComponent<Button>();
    }

    public void OnClickStart()
    {
        OpenPopup();
    }


    ///////
    void OnClickYes()
    {
        Debug.Log("종료했다고 치자");
        
        ClosePopup();
    }

    void OnClickNo()
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
