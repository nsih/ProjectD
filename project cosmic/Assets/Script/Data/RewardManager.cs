using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class RewardManager : MonoBehaviour
{
    GameObject rewardPopup;
    Button btnReward0;
    Button btnReward1;
    Button btnReward2;

    GameObject rewardAfterPopup;


    public static List<ArtifactData> rewardArtifactList = new();
    public static List<ArtifactData> rewardActionList = new();


    public void OpenArtifactRewardPopup()
    {
        rewardPopup = GameObject.Find("LandUICanvas").transform.Find("RewardPopup").gameObject;
        btnReward0 = rewardPopup.transform.Find("Reward0").gameObject.GetComponent<Button>();
        btnReward1 = rewardPopup.transform.GetChild(2).GetComponent<Button>();
        btnReward2 = rewardPopup.transform.GetChild(3).GetComponent<Button>();

        rewardAfterPopup = GameObject.Find("LandUICanvas").transform.Find("RewardAfterPopup").gameObject;
    }

    public void OnClickRewardBtn(Button clickedButton)
    {
        ArtifactData clickedArtifact;

        if (clickedButton.name[^1] == '0')
            clickedArtifact = rewardArtifactList[0];

        else
            clickedArtifact = rewardArtifactList[1];

        //ApplayArtifact(clickedArtifact);
        ShowArtifactSum(clickedArtifact);

        ClosePopup();
    }
}




public class RewardData
{
    RewardType rewardType;
    public string testResultName;
    [TextArea(2, 5)]
    public string resultText;
}


enum RewardType
{
    Artifact,
    Action
}