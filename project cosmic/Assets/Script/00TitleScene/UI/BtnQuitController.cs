using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//누르면 겜 종료
public class BtnQuitController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject quitCheckPanel;

    private void Start() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        quitCheckPanel = pnlBackGround.transform.Find("QuitCheckPanel").gameObject;
    }

    public void EnableClick()
    {
        quitCheckPanel.SetActive(true);
    }
    public void DisableClick()
    {
        quitCheckPanel.SetActive(false);
    }
    public void QuitClick()
    {
        Application.Quit();
    }
}
