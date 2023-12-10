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


    public static List<ItemData> rewardItemList = new();
    public static List<ActionData> rewardActionList = new();


    public void OpenItemRewardPopup()
    {
        rewardPopup = GameObject.Find("LandUICanvas").transform.Find("RewardPopup").gameObject;
        btnReward0 = rewardPopup.transform.Find("Reward0").gameObject.GetComponent<Button>();
        btnReward1 = rewardPopup.transform.Find("Reward1").gameObject.GetComponent<Button>();
        btnReward2 = rewardPopup.transform.Find("Reward2").gameObject.GetComponent<Button>();

        rewardAfterPopup = GameObject.Find("LandUICanvas").transform.Find("RewardAfterPopup").gameObject;
    }



    ///////
    

    void SuggestReward()
    {
        List<ActionData> suggestableActionList = rewardActionList.Except(PlayerInfo.playerActionList).ToList();
        List<ItemData> suggestableItemList = rewardItemList.Except(PlayerInfo.playerItemList).ToList();

        for (int i = 0; i < 3; i++) // 3번 반복
        {
            List<object> selectedList;
            int selectedIndex;

            GameObject btnReward = gameObject;

            if(i == 0)
                btnReward = btnReward0.gameObject;
            else if(i == 1)
                btnReward = btnReward1.gameObject;
            else if(i == 2)
                btnReward = btnReward2.gameObject;
            else
                Debug.Log("index error");

            // 두 리스트 중 랜덤으로 하나 선택
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                selectedList = suggestableActionList.ConvertAll(x => (object)x);
                selectedIndex = UnityEngine.Random.Range(0, selectedList.Count);
                Debug.Log($"Selected Action List: {selectedList[selectedIndex]}, Index: {selectedIndex}");

                btnReward.gameObject.transform.Find("ItemIMG").gameObject.GetComponent<Image>().sprite = 
                    suggestableActionList[selectedIndex].sprite;

                btnReward.gameObject.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text =
                    suggestableActionList[selectedIndex].name;

                btnReward.gameObject.transform.Find("Type").gameObject.GetComponent<TMP_Text>().text = "Action";

                btnReward.gameObject.transform.Find("Comment").gameObject.GetComponent<TMP_Text>().text =
                    suggestableActionList[selectedIndex].beforeComment;


            }
            else
            {
                selectedList = suggestableItemList.ConvertAll(x => (object)x);
                selectedIndex = UnityEngine.Random.Range(0, selectedList.Count);
                Debug.Log($"Selected Item List: {selectedList[selectedIndex]}, Index: {selectedIndex}");
            }
        }
    }
}