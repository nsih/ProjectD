using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAchievementController : MonoBehaviour
{
    GameObject PnlBackGround;
    GameObject AchievementView;

    private void Awake() 
    {
        PnlBackGround = GameObject.Find("PnlBackGround");
        AchievementView = PnlBackGround.transform.Find("AchievementView").gameObject;
    }
    public void OnClick()
    {
        AchievementView.SetActive(true);
    }
}
