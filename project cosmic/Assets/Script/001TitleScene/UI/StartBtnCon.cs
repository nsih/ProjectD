using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtnCon : MonoBehaviour
{
    GameObject GameManagerObj;
    GameObject pnlBackGround;


    void Start()
    {
        GameManagerObj = GameObject.Find("GameManager");
        pnlBackGround = GameObject.Find("PnlBackGround");
    }

    public void OnclickStart()
    {
        SceneManager.LoadScene("ScnRoom");
    }
}
