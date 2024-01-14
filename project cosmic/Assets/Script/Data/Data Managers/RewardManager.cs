using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using System.IO;

public class RewardManager : MonoBehaviour
{
    GameObject rewardPopup;
    Button btnReward0;
    Button btnReward1;
    Button btnReward2;

    GameObject rewardAfterPopup;


    public List<ItemData> rewardItemList = new();
    public List<ActionData> rewardActionList = new();

    public List<object> suggestedList = new List<object>();


    void Awake() 
    {
        LoadItemData("Assets/Script/Data/0.Item");
        LoadActionData("Assets/Script/Data/1.Action");
    }

    public void OpenRewardPopup()
    {
        rewardPopup = GameObject.Find("LandUICanvas").transform.Find("RewardPopup").gameObject;
        btnReward0 = rewardPopup.transform.Find("Reward0").gameObject.GetComponent<Button>();
        btnReward1 = rewardPopup.transform.Find("Reward1").gameObject.GetComponent<Button>();
        btnReward2 = rewardPopup.transform.Find("Reward2").gameObject.GetComponent<Button>();

        rewardAfterPopup = GameObject.Find("LandUICanvas").transform.Find("RewardAfterPopup").gameObject;

        rewardPopup.SetActive(true);

        SuggestReward();

        btnReward0.onClick.AddListener(OnClickRewardBtn);
        btnReward1.onClick.AddListener(OnClickRewardBtn);
        btnReward2.onClick.AddListener(OnClickRewardBtn);
    }

    public void CloseRewardPopup(object reward)
    {
        btnReward0.onClick.RemoveAllListeners();
        btnReward1.onClick.RemoveAllListeners();
        btnReward2.onClick.RemoveAllListeners();
        rewardPopup.SetActive(false);


        if(reward is ItemData )
            {
                ItemData itemData = (ItemData)reward;

                rewardAfterPopup.transform.Find("Image").GetComponent<Image>().sprite = itemData.sprite;
                rewardAfterPopup.transform.Find("Text").GetComponent<TMP_Text>().text = itemData.afterComment;
            }

            else if(reward is ActionData)
            {
                ActionData actionData = (ActionData)reward;

                rewardAfterPopup.transform.Find("Image").GetComponent<Image>().sprite = actionData.icon;
                rewardAfterPopup.transform.Find("Text").GetComponent<TMP_Text>().text = actionData.afterComment;
            }

        rewardAfterPopup.SetActive(true);
        Invoke( "CloseAfterRewardPopup" , 3f);
    }

    public void CloseAfterRewardPopup()
    {
        rewardAfterPopup.transform.Find("Image").GetComponent<Image>().sprite = null;
        rewardAfterPopup.transform.Find("Text").GetComponent<TMP_Text>().text = null;

        rewardAfterPopup.SetActive(false);
    }


    ///Reward Item List
    void LoadItemData(string folderPath)
    {
        // 폴더 내의 모든 파일 경로 가져오기
        string[] filePaths = Directory.GetFiles(folderPath, "*.asset");

        foreach (string filePath in filePaths)
        {
            // ScriptableObject 로드
            ItemData itemData = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemData>(filePath);

            if (itemData != null)
            {
                // 리스트에 추가
                rewardItemList.Add(itemData);
            }
            else
            {
                Debug.LogWarning("Failed to load ItemData from file: " + filePath);
            }
        }
    }
    //Reward Action List
    void LoadActionData(string folderPath)
    {
        // 폴더 내의 모든 파일 경로 가져오기
        string[] filePaths = Directory.GetFiles(folderPath, "*.asset");

        foreach (string filePath in filePaths)
        {
            // ScriptableObject 로드
            ActionData actionData = UnityEditor.AssetDatabase.LoadAssetAtPath<ActionData>(filePath);

            if (actionData != null)
            {
                // 리스트에 추가
                rewardActionList.Add(actionData);
            }
            else
            {
                Debug.LogWarning("Failed to load ItemData from file: " + filePath);
            }
        }

    }
             


    ///////
    void SuggestReward()
    {
        List<ActionData> suggestableActionList = rewardActionList.Except(PlayerInfo.playerActionList).ToList();
        List<ItemData> suggestableItemList = rewardItemList.Except(PlayerInfo.playerItemList).ToList();

        List<object> selectedList;
        suggestedList.Clear();


        /*
        리워드를 소모해서 중복 안되는 보상이 부족할때 집어넣을 temp 보상 목록 필요
        근데 보상목록이 애초에 충분하면 과연 다 쓸일이 있을까?
        */

        for (int i = 0; i < 3; i++) // 3번 반복
        {
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

            // action
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                selectedList = suggestableActionList.ConvertAll(x => (object)x);
                selectedIndex = UnityEngine.Random.Range(0, selectedList.Count);


                int maxAttempts = 1000; // 최대 시도 횟수

                do
                {
                    selectedIndex = UnityEngine.Random.Range(0, selectedList.Count);

                    maxAttempts--;
                } while (suggestedList.Contains(selectedList[selectedIndex]) &&  maxAttempts > 0);

                if (maxAttempts <= 0)
                {
                    Debug.LogError("Failed to pick a reward after maximum attempts.");
                }

                suggestedList.Add(selectedList[selectedIndex]);
                //Debug.Log($"Selected Action List: {selectedList[selectedIndex]}, Index: {selectedIndex}");

                btnReward.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().sprite = 
                    suggestableActionList[selectedIndex].icon;

                btnReward.gameObject.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text =
                    suggestableActionList[selectedIndex].actionName;

                btnReward.gameObject.transform.Find("Type").gameObject.GetComponent<TMP_Text>().text = "Action";

                btnReward.gameObject.transform.Find("Comment").gameObject.GetComponent<TMP_Text>().text =
                    suggestableActionList[selectedIndex].beforeComment;


            }
            // item
            else
            {
                selectedList = suggestableItemList.ConvertAll(x => (object)x);
                selectedIndex = UnityEngine.Random.Range(0, selectedList.Count);


                int maxAttempts = 1000;
                do
                {
                    selectedIndex = UnityEngine.Random.Range(0, selectedList.Count);
                    maxAttempts--;
                } while (suggestedList.Contains(selectedList[selectedIndex]) &&  maxAttempts > 0);

                if (maxAttempts <= 0)
                {
                    Debug.LogError("Failed to pick a reward after maximum attempts.");
                }

                suggestedList.Add(selectedList[selectedIndex]);
                //Debug.Log($"Selected Item List: {selectedList[selectedIndex]}, Index: {selectedIndex}");

                btnReward.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().sprite = 
                    suggestableItemList[selectedIndex].sprite;

                btnReward.gameObject.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text =
                    suggestableItemList[selectedIndex].itemName;

                btnReward.gameObject.transform.Find("Type").gameObject.GetComponent<TMP_Text>().text = "Item";

                btnReward.gameObject.transform.Find("Comment").gameObject.GetComponent<TMP_Text>().text =
                    suggestableItemList[selectedIndex].beforeComment;
            }
        }
    }

    void OnClickRewardBtn()
    {
        GameObject btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().gameObject;
        int rewardIndex = int.Parse(btn.name[btn.name.Length - 1].ToString());

        if (rewardIndex >= 0 && rewardIndex < suggestedList.Count)
        {
            if(suggestedList[rewardIndex] is ItemData )
            {
                ItemData itemData = (ItemData)suggestedList[rewardIndex];
                PlayerInfo.playerItemList.Add(itemData);

                this.gameObject.GetComponent<PlayerInfo>().OutcomeOffsetApply(itemData.outcomeOffset);
            }

            else if(suggestedList[rewardIndex] is ActionData)
            {
                ActionData actionData = (ActionData)suggestedList[rewardIndex];
                PlayerInfo.playerActionList.Add(actionData);
            }
        }

        else
        {
            Debug.Log("Invalid rewardIndex : "+rewardIndex);
        }

        CloseRewardPopup(suggestedList[rewardIndex]);
    }
}