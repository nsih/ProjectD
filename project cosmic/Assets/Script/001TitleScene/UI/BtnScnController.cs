using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnScnController : MonoBehaviour
{
    GameObject GameManagerObj;
    GameObject pnlBackGround;


    private void Start() 
    {
        GameManagerObj = GameObject.Find("GameManager");
        pnlBackGround = GameObject.Find("PnlBackGround");
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

}
