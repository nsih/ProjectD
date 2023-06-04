using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAchievementController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject achievementView;

    private void Awake() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        achievementView = pnlBackGround.transform.Find("AchievementView").gameObject;
    }
    public void OnClick()
    {
        achievementView.SetActive(true);
    }
}
