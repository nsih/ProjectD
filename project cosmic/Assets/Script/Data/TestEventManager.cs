using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using Random=UnityEngine.Random;
using Unity.PlasticSCM.Editor.WebApi;

public class TestEventManager : MonoBehaviour
{
    GameObject eventCanvas;
    GameObject testPopup;
    Image eventIMG;
    GameObject eventText;

    GameObject testBtn;
    GameObject testInfoText;

    GameObject testDicePopup;
    GameObject dicePack;


    public static bool isTesting;
    
    TestEventData currentTestEventData;

    public List<TestEventData> Stage1FixedEventList = new List<TestEventData>();
    public List<TestEventData> Stage1RandomEventList = new List<TestEventData>();

    void Start()
    {
        isTesting = false;
    }


    public void StartRandomTestEvent(int _currentStage)
    {
        //
        eventCanvas = GameObject.Find("EventCanvas");
        testPopup = eventCanvas.gameObject.transform.Find("TestPopup").gameObject;
        eventIMG= testPopup.gameObject.transform.Find("EventIMG").gameObject.GetComponent<Image>();
        eventText = eventCanvas.gameObject.transform.Find("EventText").gameObject;
        testBtn = eventCanvas.gameObject.transform.Find("TestBtn").gameObject;
        testInfoText = eventCanvas.gameObject.transform.Find("TestInfoText").gameObject;
        testDicePopup = eventCanvas.gameObject.transform.Find("TestDicePopup").gameObject;

        isTesting = true;

        currentTestEventData = GetEventData(_currentStage);

        eventIMG.sprite = currentTestEventData.testSprite;
        eventText.GetComponent<TMP_Text>().text = currentTestEventData.testText;
        //testBtn
        testInfoText.GetComponent<TMP_Text>().text = currentTestEventData.testTypeS;

        ////dice pack 골라서 할당하고 활성화
        GetDicePack();        
    }

    void EndTestEvent() //버튼 주사위 팝업 초기화
    {
        isTesting = false;
        currentTestEventData = null;

        dicePack = null;
    }

    TestEventData GetEventData(int _currentStage)
    {
        int eventIndex = 0;
        bool eventPicked = false;


        switch(_currentStage)
        {
            //stage 1 Random Event
            case 1:
                while(!eventPicked)
                {
                    eventIndex = Random.Range(0, Stage1FixedEventList.Count);

                    if(Stage1FixedEventList[eventIndex].isTested == false)
                    {
                        Stage1FixedEventList[eventIndex].isTested = true;
                        eventPicked = true;
                    }
                }
                return Stage1FixedEventList[eventIndex];


            //?
            default:
                Debug.Log("Stage error : stage"+_currentStage);
                return Stage1FixedEventList[eventIndex];
        }
    }


    //dice pack 골라서 할당하고 활성화
    void GetDicePack()
    {
        int diceCount = 1;
        dicePack = null;

        //TestType에 따라 계산
        if(currentTestEventData.testType == TestType.physical)
        {
            diceCount = PlayerInfo.physical + currentTestEventData.testOffset;
        }
        else if(currentTestEventData.testType == TestType.willPower)
        {
            diceCount = PlayerInfo.willPower + currentTestEventData.testOffset;
        }
        else if(currentTestEventData.testType == TestType.knowledge)
        {
            diceCount = PlayerInfo.knowledge + currentTestEventData.testOffset;
        }
        else if(currentTestEventData.testType == TestType.charm)
        {
            diceCount = PlayerInfo.charm + currentTestEventData.testOffset;
        }

        //계산된 다이스 개수에 따라 dice pack 활성화
        if(diceCount == 1)
        {
            dicePack = testDicePopup.transform.Find("Pack1").gameObject;
        }
        else if(diceCount == 2)
        {
            dicePack = testDicePopup.transform.Find("Pack2").gameObject;
        }
        else if(diceCount == 3)
        {
            dicePack = testDicePopup.transform.Find("Pack3").gameObject;
        }
        else if(diceCount == 4)
        {
            dicePack = testDicePopup.transform.Find("Pack4").gameObject;
        }
        else if(diceCount == 5)
        {
            dicePack = testDicePopup.transform.Find("Pack5").gameObject;
        }
        else if(diceCount == 6)
        {
            dicePack = testDicePopup.transform.Find("Pack6").gameObject;
        }
        else
        {
            Debug.Log("diceCount Error");
        }

        dicePack.SetActive(true);
    }
    void RollDice()
    {

    }


    //랜덤 이벤트 사용기록 소거
    public void InitializeRandomEventIsTested(int _currentStage)
    {
        switch(_currentStage)
        {
            //stage 1 Random Event
            case 1:
                //stage 1
                foreach(var i in Stage1RandomEventList)
                {
                    i.isTested = false;
                }
                break;


            //?
            default:
                Debug.Log("Stage error : stage"+_currentStage);
                break;
        }

    }
}
