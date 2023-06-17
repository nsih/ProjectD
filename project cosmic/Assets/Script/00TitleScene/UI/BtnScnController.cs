using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnScnController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject pnlTitleCheck;


    private void Start() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        pnlTitleCheck = pnlBackGround.transform.Find("PnlTitleCheck").gameObject;
    }

    //Scene Change
    public void TitleScnChange()
    {
        SceneManager.LoadScene("ScnTitle");
    }
    public void RoomScnChange()
    {
        SceneManager.LoadScene("ScnRoom");
    }
    public void LandScnChange()
    {
        SceneManager.LoadScene("ScnLand");
    }

    public void EnableTitleScnCheck()
    {
        pnlTitleCheck.SetActive(true);
    }
    public void DisableTitleScnCheck()
    {
        pnlTitleCheck.SetActive(false);
    }
}
