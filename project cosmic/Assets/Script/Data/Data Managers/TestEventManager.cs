using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using Unity.PlasticSCM.Editor.WebApi;

public class TestEventManager : MonoBehaviour
{
    GameObject gameManager;
    GameObject eventCanvas;
    GameObject testPopup;
    Image eventIMG;

    GameObject eventTitle;
    GameObject eventText;

    GameObject testBtn;
    GameObject testInfoText;

    GameObject testDicePopup;
    GameObject dicePack;

    public static bool isCurrentResultSuccess;

    TestEventData currentTestEventData;

    public List<TestEventData> Stage1FixedEventList = new List<TestEventData>();
    public List<TestEventData> Stage1RandomEventList = new List<TestEventData>();

    void Start()
    {
        GameManager.isTesting = false;
        isCurrentResultSuccess = false;
    }

    #region "Random Event"
    //랜덤이벤트 시작
    public void StartRandomTestEvent(int _currentStage)
    {
        //object initialize
        eventCanvas = GameObject.Find("EventCanvas");
        testPopup = eventCanvas.gameObject.transform.Find("TestPopup").gameObject;
        eventIMG = testPopup.gameObject.transform.Find("EventIMG").gameObject.GetComponent<Image>();
        eventTitle = testPopup.gameObject.transform.Find("EventTitle").gameObject;
        eventText = testPopup.gameObject.transform.Find("EventText").gameObject;
        testBtn = testPopup.gameObject.transform.Find("TestButton").gameObject;
        testInfoText = testBtn.gameObject.transform.Find("TestInfoText").gameObject;
        testDicePopup = testPopup.gameObject.transform.Find("DicePopUp").gameObject;


        //
        GameManager.isTesting = true;
        currentTestEventData = GetRandomEventData(_currentStage);


        //show
        testPopup.SetActive(true);
        eventIMG.sprite = currentTestEventData.testSprite;
        eventTitle.GetComponent<TMP_Text>().text = currentTestEventData.testName;
        eventText.GetComponent<TMP_Text>().text = currentTestEventData.testText;

        testInfoText.GetComponent<TMP_Text>().text = currentTestEventData.testTypeS;

        ////dice pack 골라서 할당하고 활성화
        //GetDicePack();

        //dicePack
        testBtn.GetComponent<Button>().onClick.AddListener(OnClickDiceRoll);
    }

    //랜덤 이벤트 하나 뽑기
    TestEventData GetRandomEventData(int _currentStage)
    {
        int eventIndex = 0;
        bool eventPicked = false;


        switch (_currentStage)
        {
            //stage 1 Random Event
            case 1:
                while (!eventPicked)
                {
                    eventIndex = Random.Range(0, Stage1RandomEventList.Count);

                    if (Stage1RandomEventList[eventIndex].isTested == false)
                    {
                        Stage1RandomEventList[eventIndex].isTested = true;
                        eventPicked = true;
                    }
                }
                return Stage1RandomEventList[eventIndex];


            //?
            default:
                Debug.Log("Stage error : stage" + _currentStage);
                return Stage1RandomEventList[eventIndex];
        }
    }
    #endregion



    /*
    //dice pack 골라서 할당하고 활성화
    void GetDicePack()
    {
        int diceCount = 1;
        dicePack = null;

        //TestType에 따라 계산

        
        //physical
        if (currentTestEventData.testType == TestType.Physical)
        {
            diceCount = PlayerInfo.physical + currentTestEventData.testOffset;
        }
        //mental
        else if (currentTestEventData.testType == TestType.Mental)
        {
            diceCount = PlayerInfo.mental + currentTestEventData.testOffset;
        }
        //charm
        else if (currentTestEventData.testType == TestType.Charm)
        {
            diceCount = PlayerInfo.charm + currentTestEventData.testOffset;
        }
        
        //None
        else
        {
            return;
        }

        //계산된 다이스 개수에 따라 dice pack 활성화
        if (diceCount == 1)
        {
            dicePack = testDicePopup.transform.Find("Pack1").gameObject;
        }
        else if (diceCount == 2)
        {
            dicePack = testDicePopup.transform.Find("Pack2").gameObject;
        }
        else if (diceCount == 3)
        {
            dicePack = testDicePopup.transform.Find("Pack3").gameObject;
        }
        else if (diceCount == 4)
        {
            dicePack = testDicePopup.transform.Find("Pack4").gameObject;
        }
        else if (diceCount == 5)
        {
            dicePack = testDicePopup.transform.Find("Pack5").gameObject;
        }
        else if (diceCount == 6)
        {
            dicePack = testDicePopup.transform.Find("Pack6").gameObject;
        }
        else
        {
            Debug.Log("diceCount Error");
        }

        dicePack.SetActive(true);
    }
    */

    //다이스 롤 버튼
    void OnClickDiceRoll()
    {
        if (dicePack)
        {
            testBtn.GetComponent<Button>().interactable = false;
            for (int i = 0; i < dicePack.transform.childCount; i++)
            {
                //StartCoroutine(dicePack.transform.GetChild(i).gameObject.GetComponent<CubeRotation>().RotateCube(dicePack.transform.GetChild(i).gameObject));
            }

            Invoke("CheckSuccess", 3.0f);
        }

        else
        {
            Debug.Log("dicePack Null Error");
        }
    }

    void CheckSuccess()
    {
        // 버튼 동작 변경
        testBtn.GetComponent<Button>().interactable = true;

        if (isCurrentResultSuccess)
        {
            testInfoText.GetComponent<TMP_Text>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            testInfoText.GetComponent<TMP_Text>().text = "Success";
        }
        else
        {
            testInfoText.GetComponent<TMP_Text>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            testInfoText.GetComponent<TMP_Text>().text = "Fail";
        }
        testBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        testBtn.GetComponent<Button>().onClick.AddListener(OnClickExecuteEventResult);
    }





    //결과실행 버튼
    void OnClickExecuteEventResult()
    {
        int resultIndex;
        //show
        if (isCurrentResultSuccess)
            resultIndex = 0;
        else
            resultIndex = 1;

        //Show
        eventIMG.sprite = currentTestEventData.results[resultIndex].resultSprite;
        eventTitle.GetComponent<TMP_Text>().text = currentTestEventData.results[resultIndex].testResultName;
        eventText.GetComponent<TMP_Text>().text = currentTestEventData.results[resultIndex].resultText;
        testInfoText.GetComponent<TMP_Text>().text = "";

        //event result offset apply
        this.gameObject.GetComponent<PlayerInfo>().
        OutcomeOffsetApply(currentTestEventData.results[resultIndex].eventOffset);

        //버튼 바꾸기
        testInfoText.GetComponent<TMP_Text>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        testInfoText.GetComponent<TMP_Text>().text = "종료";
        testBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        testBtn.GetComponent<Button>().onClick.AddListener(OnClickEndEvent);
    }


    //

    //이벤트 종료 버튼
    public void OnClickEndEvent()
    {
        //쓴주사위 눈 ?로 돌려놓고 끄기
        for (int i = 0; i < dicePack.transform.childCount; i++)
        {
            dicePack.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "?";
        }

        //
        GameManager.isTesting = false;
        isCurrentResultSuccess = false;

        //
        currentTestEventData = null;
        testPopup.SetActive(false);
        dicePack.SetActive(false);
        dicePack = null;
        testBtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    //랜덤 이벤트 사용기록 소거 (스테이지 시작시)
    public void InitializeRandomEventIsTested(int _currentStage)
    {
        switch (_currentStage)
        {
            //stage 1 Random Event
            case 1:
                //stage 1
                foreach (var i in Stage1RandomEventList)
                {
                    i.isTested = false;
                }
                break;


            //?
            default:
                Debug.Log("Stage error : stage" + _currentStage);
                break;
        }

    }



    //보통 랜덤 이벤트 사용 기록 소거 (게임 처음부터 다시 시작시)
    public void InitializeCommonEventIsTested()
    {
        //CommonEventList
    }
}
