using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class ArtifactManager : MonoBehaviour
{
    //보상
    GameObject artifactRewardPopup;         //전체
    GameObject artifactSum;          //고르고 난뒤 뜨는거

    Button btnRewardArtifact0;
    Button btnRewardArtifact1;

    Button btnCloseRewardPopup;



    //보유 유물 리스트
    GameObject artifactListPopup;           //내꺼
    List<GameObject> iconList = new();              //아이콘

    Button btnCloseListPopup;


    public List<ArtifactData> allArtifactList;  //전체 아티팩트 리스트
    public static List<ArtifactData> playerArtifactList = new();   //이번 게임에서 얻은 유물 리스트



    public static List<ArtifactData> rewardArtifactList = new();


    //해금된 아티팩트 리스트 (생략)
    //public List<ArtifactData> ingameArtifactList = new(); //해금된 (인게임에서 쓸 수 있는) 유물리스트 (w)

    #region "Reward"
    public void OpenArtifactRewardPopup()
    {
        artifactRewardPopup = GameObject.Find("PnlBackGround").transform.Find("ArtifactRewordPopup").gameObject;
        btnRewardArtifact0 = artifactRewardPopup.transform.GetChild(1).GetComponent<Button>();
        btnRewardArtifact1 = artifactRewardPopup.transform.GetChild(2).GetComponent<Button>();
        btnCloseRewardPopup = artifactRewardPopup.transform.GetChild(3).GetComponent<Button>();
        artifactSum = GameObject.Find("PnlBackGround").transform.Find("ArtifactSumPanel").gameObject;



        artifactRewardPopup.SetActive(true);

        btnRewardArtifact0.onClick.AddListener(() => ClickRewardArtifact(btnRewardArtifact0));
        btnRewardArtifact1.onClick.AddListener(() => ClickRewardArtifact(btnRewardArtifact1));
        btnCloseRewardPopup.onClick.AddListener(ClosePopup);
    }

    public void ClickRewardArtifact(Button clickedButton)
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

    public void ClosePopup()
    {
        artifactRewardPopup.SetActive(false);

        btnRewardArtifact0.onClick.RemoveAllListeners();
        btnRewardArtifact1.onClick.RemoveAllListeners();
        btnCloseRewardPopup.onClick.RemoveAllListeners();
    }


    private void ShowArtifactSum(ArtifactData artifactData)
    {
        artifactSum.SetActive(true);
        artifactSum.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = artifactData.artifactSprite;
        artifactSum.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = artifactData.afterComment;



        Invoke("DeactivateArtifactSum", 1.5f);
    }

    private void DeactivateArtifactSum()
    {
        artifactSum.SetActive(false);
    }

    public static List<ArtifactData> GetRandomArtifacts(List<ArtifactData> listA, List<ArtifactData> listB) //내가 없는 유물 2개 차출
    {
        List<ArtifactData> missingArtifacts = listA.Except(listB).ToList();

        if (missingArtifacts.Count < 2)
        {
            throw new InvalidOperationException("Not enough missing artifacts to select from.");
        }

        // Fisher-Yates shuffle
        for (int i = missingArtifacts.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            ArtifactData temp = missingArtifacts[i];
            missingArtifacts[i] = missingArtifacts[j];
            missingArtifacts[j] = temp;
        }

        return missingArtifacts.Take(2).ToList();
    }
    #endregion

    #region "Mine List"


    public void CloseListPopup()
    {
        artifactListPopup.SetActive(false);

        for (int i = 0; i < iconList.Count; i++)
        {
            iconList[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }


        iconList.Clear();



        btnCloseListPopup.onClick.RemoveAllListeners();
    }



    #endregion

}
