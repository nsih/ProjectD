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



    //가진거 리스트
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
        artifactSum.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = artifactData.commentText;



        Invoke("DeactivateArtifactSum", 1.5f);
    }

    private void DeactivateArtifactSum()
    {
        artifactSum.SetActive(false);
    }


    /*
    public void SuggestArtifacts() //리워드 보여주기기기기기
    {
        rewardArtifactList = GetRandomArtifacts(allArtifactList,playerArtifactList);

        //1 - sprite, 2 - Name, 3 - bp


        GameObject sprite0 = btnRewardArtifact0.gameObject.transform.GetChild(1).gameObject;
        sprite0.GetComponent<Image>().sprite = rewardArtifactList[0].artifactSprite;

        GameObject name0 = btnRewardArtifact0.gameObject.transform.GetChild(2).gameObject;
        name0.GetComponent<TextMeshProUGUI>().text = rewardArtifactList[0].artifactName;

        GameObject text0 = btnRewardArtifact0.gameObject.transform.GetChild(3).gameObject;
        text0.GetComponent<TextMeshProUGUI>().text =    
                                                        "<color=#24ED50>" + rewardArtifactList[0].benefitText + "</color>"
                                                        + "\n\n" + 
                                                        "<color=#ED4524>" + rewardArtifactList[0].penaltyText + "</color>";



        GameObject sprite1 = btnRewardArtifact1.gameObject.transform.GetChild(1).gameObject;
        sprite1.GetComponent<Image>().sprite = rewardArtifactList[1].artifactSprite;

        GameObject name1 = btnRewardArtifact1.gameObject.transform.GetChild(2).gameObject;
        name1.GetComponent<TextMeshProUGUI>().text = rewardArtifactList[1].artifactName;

        GameObject text1 = btnRewardArtifact1.gameObject.transform.GetChild(3).gameObject;
        text1.GetComponent<TextMeshProUGUI>().text = 
                                                        "<color=#24ED50>" + rewardArtifactList[1].benefitText + "</color>"
                                                        + "\n\n" + 
                                                        "<color=#ED4524>" + rewardArtifactList[1].penaltyText + "</color>";




    }
    */

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
    /*
    void ApplayArtifact(ArtifactData artifactData)
    {
        //공격 변경
        if (artifactData.isAttackReward)
        {
            Debug.Log("공격 달라짐" + artifactData.attackID);
        }
        //행동 추가
        if (artifactData.isActionReward)
        {
            Debug.Log("행동추가" + artifactData.actionID);
        }
        //친구 추가
        if (artifactData.isCompanionReward)
        {
            Debug.Log("친구추가" + artifactData.companionID);
        }


        //physical
        if (artifactData.physicalOffset != 0)
        {
            gameObject.GetComponent<PlayerInfo>().PhysicalModify(artifactData.physicalOffset);
        }
        //mental
        if (artifactData.mentalOffset != 0)
        {
            gameObject.GetComponent<PlayerInfo>().MentalModify(artifactData.mentalOffset);
        }
        //charm
        if (artifactData.charmOffset != 0)
        {
            gameObject.GetComponent<PlayerInfo>().CharmModify(artifactData.charmOffset);
        }

        //max hp
        if (artifactData.maxHPOffset != 0)
        {
            PlayerInfo.maxHpOffset = PlayerInfo.maxHpOffset + artifactData.maxHPOffset;
            gameObject.GetComponent<PlayerInfo>().MaxHpCalc();
        }
        //hp
        if (artifactData.hPOffset != 0)
        {
            gameObject.GetComponent<PlayerInfo>().HpModify(artifactData.hPOffset);
        }

        //max action point
        if (artifactData.maxActionPointOffset != 0)
        {
            PlayerInfo.maxAPOffset = PlayerInfo.maxAPOffset + artifactData.maxActionPointOffset;
            gameObject.GetComponent<PlayerInfo>().MaxAPCalc();
        }
        //action point
        if (artifactData.sanityOffset != 0)
        {
            gameObject.GetComponent<PlayerInfo>().APModify(artifactData.actionPointOffset);
        }

        //coin
        if (artifactData.coinOffset != 0)
        {
            PlayerInfo.coin = +artifactData.coinOffset;
        }

        //DMG
        

        //이속 조정
        if (artifactData.speedOffset != 0)
        {
            PlayerInfo.playerMoveSpeed = PlayerInfo.playerMoveSpeed * artifactData.speedOffset;
        }


        //시야 (카메라)
        if (artifactData.charmOffset != 0)
        {
            GameObject camera = GameObject.Find("Main Camera");

            camera.GetComponent<Camera>().orthographicSize += 7;
        }


        //플레이어 유물 추가
        playerArtifactList.Add(artifactData);
    }
    */
    #endregion

    #region "Mine List"
    /*
    public void OpenArtifactListPopup()
    {
        artifactListPopup = GameObject.Find("PnlBackGround").transform.Find("ArtifactList").gameObject;
        btnCloseListPopup = artifactListPopup.transform.GetChild(2).GetComponent<Button>();

        GameObject iconListOBJ = artifactListPopup.transform.GetChild(0).gameObject;

        artifactListPopup.SetActive(true);


        for (int i = 0; i < iconListOBJ.transform.childCount; i++)
        {
            GameObject childObject = iconListOBJ.transform.GetChild(i).gameObject;
            if (childObject != null)
            {
                iconList.Add(childObject);
            }

            if (i >= playerArtifactList.Count)
            {
                iconList[i].gameObject.SetActive(false);
            }
            else
            {
                Debug.Log(i);
                Debug.Log(playerArtifactList[i].artifactName);

                int index = i; // 임시 변수에 i 할당
                iconList[i].GetComponent<Button>().onClick.AddListener(() => ClickArtifactIcon(playerArtifactList[index]));

                iconList[i].gameObject.SetActive(true);
            }
        }


        btnCloseListPopup.onClick.AddListener(CloseListPopup);
    }
    */

    /*
    public void ClickArtifactIcon(ArtifactData artifactData) //아이콘 클릭시 상세정보 출력
    {
        GameObject artifactInfoOBJ = artifactListPopup.transform.GetChild(1).gameObject;        

        artifactInfoOBJ.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = artifactData.artifactSprite;
        artifactInfoOBJ.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = artifactData.artifactName;
        artifactInfoOBJ.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = artifactData.commentText;
        artifactInfoOBJ.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().text = 
                                                        "<color=#24ED50>" + artifactData.benefitText + "</color>"
                                                        + "\n\n" + 
                                                        "<color=#ED4524>" + artifactData.penaltyText + "</color>";
    }
    */


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
