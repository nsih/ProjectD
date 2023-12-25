using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using Unity.PlasticSCM.Editor.WebApi;

public class ActionManager : MonoBehaviour
{
    GameObject gameManager;
    GameObject landUICanvas;
    GameObject eventCanvas;
    GameObject testPopup;
    Image eventIMG;

    GameObject eventTitle;
    GameObject eventText;

    GameObject testBtn;
    GameObject testInfoText;

    GameObject testDicePopup;
    GameObject dicePack;

    ActionData currentActionData;

    public static bool isCurrentResultSuccess;

    void Start()
    {        
        GameManager.isTesting = false;
        isCurrentResultSuccess = false;
    }


    public void StartActionTestEvent(int actionIndex)
    {
        GameObject.Find("LandUICanvas").GetComponent<LandUICon>().ActionListSwitch();

        //object initialize
        eventCanvas = GameObject.Find("EventCanvas");
        testPopup = eventCanvas.gameObject.transform.Find("TestPopup").gameObject;
        eventIMG = testPopup.gameObject.transform.Find("EventIMG").gameObject.GetComponent<Image>();
        eventTitle = testPopup.gameObject.transform.Find("EventTitle").gameObject;
        eventText = testPopup.gameObject.transform.Find("EventText").gameObject;
        testBtn = testPopup.gameObject.transform.Find("TestButton").gameObject;
        testInfoText = testBtn.gameObject.transform.Find("TestInfoText").gameObject;
        testDicePopup = testPopup.gameObject.transform.Find("DicePopUp").gameObject;

        GameManager.isTesting = true;
        

        Debug.Log(actionIndex);
        currentActionData = PlayerInfo.playerActionList[actionIndex];

        //show
        testPopup.SetActive(true);
        eventIMG.sprite = currentActionData.sprite;
        eventTitle.GetComponent<TMP_Text>().text = currentActionData.name;
        eventText.GetComponent<TMP_Text>().text = currentActionData.actionText;
        testInfoText.GetComponent<TMP_Text>().text = currentActionData.testTypeS + "시작";

        ////dice pack 골라서 할당하고 활성화
        GetDicePack();

        //dicePack
        testBtn.GetComponent<Button>().onClick.AddListener(OnClickDiceRoll);
    }


    void GetDicePack()
    {
        int diceCount = 1;
        dicePack = null;

        //TestType에 따라 계산

        //physical
        if (currentActionData.testType == TestType.Physical)
        {
            diceCount = PlayerInfo.physical + currentActionData.testOffset;
        }
        //mental
        else if (currentActionData.testType == TestType.Mental)
        {
            diceCount = PlayerInfo.mental + currentActionData.testOffset;
        }
        //charm
        else if (currentActionData.testType == TestType.Charm)
        {
            diceCount = PlayerInfo.charm + currentActionData.testOffset;
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
        else if (diceCount >= 6)
        {
            dicePack = testDicePopup.transform.Find("Pack6").gameObject;
        }
        else
        {
            Debug.Log("diceCount Error");
        }

        dicePack.SetActive(true);
    }

    void OnClickDiceRoll()
    {
        if (dicePack)
        {
            testBtn.GetComponent<Button>().interactable = false;
            for (int i = 0; i < dicePack.transform.childCount; i++)
            {
                StartCoroutine(dicePack.transform.GetChild(i).gameObject.GetComponent<CubeRotation>().RotateCube(dicePack.transform.GetChild(i).gameObject));
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


    void OnClickExecuteEventResult()
    {
        int resultIndex;
        //show
        if (isCurrentResultSuccess)
            resultIndex = 0;
        else
            resultIndex = 1;

        //Show
        eventIMG.sprite = currentActionData.results[resultIndex].resultSprite;
        eventTitle.GetComponent<TMP_Text>().text = currentActionData.results[resultIndex].testResultName;
        eventText.GetComponent<TMP_Text>().text = currentActionData.results[resultIndex].resultText;
        testInfoText.GetComponent<TMP_Text>().text = "";

        //event result offset apply
        this.gameObject.GetComponent<PlayerInfo>().
        OutcomeOffsetApply(currentActionData.results[resultIndex].eventOffset);

        //버튼 바꾸기
        testInfoText.GetComponent<TMP_Text>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        testInfoText.GetComponent<TMP_Text>().text = "종료";
        testBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        testBtn.GetComponent<Button>().onClick.AddListener(OnClickEndEvent);
    }

    void OnClickEndEvent()
    {
        //쓴주사위 눈 ?로 돌려놓고 끄기
        for (int i = 0; i < dicePack.transform.childCount; i++)
        {
            dicePack.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "?";
        }

        //
        GameManager.isTesting = false;
        isCurrentResultSuccess = false;


        testPopup.SetActive(false);
        dicePack.SetActive(false);
        dicePack = null;
        testBtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
