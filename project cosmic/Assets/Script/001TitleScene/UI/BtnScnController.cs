using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnScnController : MonoBehaviour
{
    GameObject GameManagerObj;
    GameObject pnlBackGround;
    GameObject pnlTitleCheck;


    private void Start() 
    {
        GameManagerObj = GameObject.Find("GameManager");
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

        GameManagerObj.GetComponent<StageManager>().StartRoomEventPhase(RoomType.Null);
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
