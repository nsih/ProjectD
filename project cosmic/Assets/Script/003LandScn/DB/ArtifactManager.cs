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
    public GameObject artifactRewardPopup;
    public GameObject artifactListPopup;





    public static List<ArtifactData> allArtifactList = new();  //전체 아티팩트 리스트

    public static List<ArtifactData> playerArtifactList = new();   //이번 게임에서 얻은 유물 리스트


    //해금된 아티팩트 리스트 (생략)


    //public List<ArtifactData> ingameArtifactList = new(); //해금된 (인게임에서 쓸 수 있는) 유물리스트 (w)



    void Start()
    {
        //artifactList[0].artifactID
    }

    void Update()
    {
        
    }

    #region "Reward Popup"


    void OpenArtifactRewardPopup()
    {
        artifactRewardPopup = GameObject.Find("LandUICanvas").transform.Find("ArtifactRewordPopup").gameObject;
        
        Button btnClose = artifactListPopup.transform.GetChild(3).GetComponent<Button>();
        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {

    }







    #endregion




    public void SuggestArtifacts() //얻을 유물 고르기
    {
        List<ArtifactData> artifactList = GetRandomArtifacts(allArtifactList,playerArtifactList);

        //1 - sprite, 2 - Name, 3 - bp


        //0
        GameObject artifactPanel0 = GameObject.Find("RewardArtifact0");

        GameObject sprite0 = artifactPanel0.gameObject.transform.GetChild(1).gameObject;
        sprite0.GetComponent<Image>().sprite = artifactList[0].artifactSprite;

        GameObject name0 = artifactPanel0.gameObject.transform.GetChild(2).gameObject;
        name0.GetComponent<TextMeshProUGUI>().text = artifactList[0].artifactName;

        GameObject text0 = artifactPanel0.gameObject.transform.GetChild(3).gameObject;
        text0.GetComponent<TextMeshProUGUI>().text = 
                                                        "<color=#ED4524>" + artifactList[0].benefitText + "</color>"
                                                        + "\n" + 
                                                        "<color=#24ED50>" + artifactList[0].penaltyText + "</color>";


        //1
        GameObject artifactPanel1 = GameObject.Find("RewardArtifact1");

        GameObject sprite1 = artifactPanel1.gameObject.transform.GetChild(1).gameObject;
        sprite0.GetComponent<Image>().sprite = artifactList[1].artifactSprite;

        GameObject name1 = artifactPanel1.gameObject.transform.GetChild(2).gameObject;
        name0.GetComponent<TextMeshProUGUI>().text = artifactList[1].artifactName;

        GameObject text1 = artifactPanel1.gameObject.transform.GetChild(3).gameObject;
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

    void ApplayArtifact(ArtifactData artifactData)
    {
        //공격 타입 바뀜
        if(artifactData.isWeaponChange)
        {
            Debug.Log("공격 달라짐" + artifactData.weaponID);
        }

        //행동 추가
        if(artifactData.isActionReword)
        {
            Debug.Log("행동추가" + artifactData.actionID);
        }

        //친구 추가
        if(artifactData.isCompanion)
        {
            Debug.Log("친구추가"+artifactData.companionID);
        }

        //최대체력 변경
        if(artifactData.maxHPOffset != 0)
        {
            gameObject.GetComponent<PlayerInfo>().MaxHpPlus(artifactData.maxHPOffset);
        }

        //풀힐
        if(artifactData.hpFullHeal)
        {
            gameObject.GetComponent<PlayerInfo>().HpModify(true);
        }

        //HP 조정
        if(artifactData.hPOffset!=0)
        {
            gameObject.GetComponent<PlayerInfo>().HpPlus(artifactData.hPOffset);
        }

        //이성수치 조정
        if(artifactData.mentalOffset!=0)
        {
            gameObject.GetComponent<GameManager>().mentalityModify(artifactData.mentalOffset);
        }

        //데미지 +
        if(artifactData.plusPlayerDamageOffset != 0)
        {
            PlayerInfo.plusAttackDamageOffset = PlayerInfo.plusAttackDamageOffset + artifactData.plusPlayerDamageOffset;

            gameObject.GetComponent<PlayerInfo>().DamageCal();
        }
        //깡뎀 *
        if(artifactData.plusPlayerDamageOffset != 0)
        {
            PlayerInfo.multiplyAttackDamageOffset = PlayerInfo.multiplyAttackDamageOffset * artifactData.plusPlayerDamageOffset;

            gameObject.GetComponent<PlayerInfo>().DamageCal();
        }

        //공격 딜레이 (공속 * )
        if(artifactData.attackDelay!=0)
        {
            PlayerInfo.attackDelay = PlayerInfo.attackDelay * artifactData.attackDelay;
        }

        //이속 조정
        if(artifactData.speedOffset!=0)
        {
            PlayerInfo.speed = PlayerInfo.speed * artifactData.speedOffset;
        }



        //육체
        if(artifactData.physicalOffset!=0)
        {
            gameObject.GetComponent<PlayerInfo>().PhysicalModify(artifactData.physicalOffset);
        }
        //의지
        if(artifactData.willPowerOffset!=0)
        {
            gameObject.GetComponent<PlayerInfo>().WillPowerModify(artifactData.willPowerOffset);
        }
        //지식
        if(artifactData.knowledgeOffset!=0)
        {
            gameObject.GetComponent<PlayerInfo>().KnowledgeModify(artifactData.knowledgeOffset);
        }
        //매력
        if(artifactData.charmOffset!=0)
        {
            gameObject.GetComponent<PlayerInfo>().CharmModify(artifactData.charmOffset);
        }

        //최대 액션스택 없긴함
        //액션스택 없긴함

        //돈
        if(artifactData.coinOffset!=0)
        {
            PlayerInfo.coin =+artifactData.coinOffset;
        }

        //시야 (카메라)
        if(artifactData.charmOffset!=0)
        {
            GameObject camera = GameObject.Find("Main Camera");

            camera.GetComponent<Camera>().orthographicSize += 7;
        }

        //기타 함수
        if(artifactData.funcID != 0)
        {
            gameObject.GetComponent<SpecialFuncManager>().SpecialFuncs(artifactData.funcID);
        }


        //플레이어 유물 추가
        playerArtifactList.Add( allArtifactList[artifactData.artifactID] );
    }

}
