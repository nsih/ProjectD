using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Achivement 창을 여는 버튼
public class BtnAchievementController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject achievementView;

    private void Awake() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        achievementView = pnlBackGround.transform.Find("AchievementView").gameObject;
    }
    public void EnableClick()
    {
        achievementView.SetActive(true);
    }

    public void DisableClick()
    {
        achievementView.SetActive(false);
    }
}
