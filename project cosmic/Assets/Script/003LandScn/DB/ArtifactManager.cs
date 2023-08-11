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
    public GameObject artifactReward;

    public List<ArtifactData> allArtifactList = new();  //전체 아티팩트 리스트
    public static List<ArtifactData> playerArtifactList = new();   //이번 게임에서 얻은 유물 리스트


    //public List<ArtifactData> ingameArtifactList = new(); //해금된 (인게임에서 쓸 수 있는) 유물리스트 (w)



    void Start()
    {
        //artifactList[0].artifactID
    }

    void Update()
    {
        
    }

    void AddArtifact()
    {

        ApplayArtifact();
    }


    void ApplayArtifact()
    {
        
    }


    void SuggestArtifacts() //버튼 클릭에서 쓸거임
    {
        List<ArtifactData> artifactList = GetRandomArtifacts(allArtifactList,playerArtifactList);

        //0 - sprite, 1 - Name, 2 - bp


        //0
        GameObject artifactPanel0 = GameObject.Find("RewardArtifact0");

        GameObject sprite0 = artifactPanel0.gameObject.transform.GetChild(0).gameObject;
        sprite0.GetComponent<Image>().sprite = artifactList[0].artifactSprite;

        GameObject name0 = artifactPanel0.gameObject.transform.GetChild(1).gameObject;
        name0.GetComponent<TextMeshProUGUI>().text = artifactList[0].artifactName;

        GameObject text0 = artifactPanel0.gameObject.transform.GetChild(2).gameObject;
        text0.GetComponent<TextMeshProUGUI>().text = 
                                                        "<color=#ED4524>" + artifactList[0].benefitText + "</color>"
                                                        + "\n" + 
                                                        "<color=#24ED50>" + artifactList[0].penaltyText + "</color>";


        //"<color=#FF0000>행동</color>"


        //1
        GameObject artifactPanel1 = GameObject.Find("RewardArtifact1");

        GameObject sprite1 = artifactPanel1.gameObject.transform.GetChild(0).gameObject;
        sprite0.GetComponent<Image>().sprite = artifactList[1].artifactSprite;

        GameObject name1 = artifactPanel1.gameObject.transform.GetChild(1).gameObject;
        name0.GetComponent<TextMeshProUGUI>().text = artifactList[1].artifactName;

        GameObject text1 = artifactPanel1.gameObject.transform.GetChild(2).gameObject;
        text0.GetComponent<TextMeshProUGUI>().text = 
                                                        "<color=#ED4524>" + artifactList[1].benefitText + "</color>"
                                                        + "\n" + 
                                                        "<color=#24ED50>" + artifactList[1].penaltyText + "</color>";




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


}
