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


    public static bool isTesting;

    public static bool isCurrentResultSuccess;
    

    public void StartActionEvent(ActionData actionData)
    {
        //object initialize
        eventCanvas = GameObject.Find("EventCanvas");
        testPopup = eventCanvas.gameObject.transform.Find("TestPopup").gameObject;

        eventIMG = testPopup.gameObject.transform.Find("EventIMG").gameObject.GetComponent<Image>();
        eventTitle = testPopup.gameObject.transform.Find("EventTitle").gameObject;
        eventText = testPopup.gameObject.transform.Find("EventText").gameObject;

        testInfoText = testPopup.gameObject.transform.Find("TestInfoText").gameObject;
        testBtn = testPopup.gameObject.transform.Find("TestButton").gameObject;

        testDicePopup = testPopup.gameObject.transform.Find("DicePopUp").gameObject;


        //
        isTesting = true;

        
        /*
        //show
        testPopup.SetActive(true);
        eventIMG.sprite = actionData.testSprite;
        eventTitle.GetComponent<TMP_Text>().text = currentTestEventData.testName;
        eventText.GetComponent<TMP_Text>().text = currentTestEventData.testText;

        testInfoText.GetComponent<TMP_Text>().text = currentTestEventData.testTypeS;

        ////dice pack 골라서 할당하고 활성화
        GetDicePack();

        //dicePack
        testBtn.GetComponent<Button>().onClick.AddListener(OnClickDiceRoll);
        */
        
    }

    
}
