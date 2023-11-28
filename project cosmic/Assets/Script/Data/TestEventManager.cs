using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TestEventManager : MonoBehaviour
{
    GameObject landUICanvas;
    GameObject eventCanvas;
    GameObject testPopup;
    Image eventIMG;
    GameObject eventText;
    


    public List<TestEventData> Stage1FixedEventList = new List<TestEventData>();
    public List<TestEventData> Stage1RandomEventList = new List<TestEventData>();


    public void StartTestEvent(int _currentStage)
    {
        landUICanvas = GameObject.Find("LandUICanvas");
        eventCanvas = GameObject.Find("EventCanvas");
        testPopup = eventCanvas.gameObject.transform.Find("TestPopup").gameObject;
        eventIMG= testPopup.gameObject.transform.Find("EventIMG").gameObject.GetComponent<Image>();
        eventText = eventCanvas.gameObject.transform.Find("EventText").gameObject;


        GetEventData(_currentStage);


        //eventText.GetComponent<TextMeshProUGUI>().text
    }

    void GetEventData(int _currentStage)
    {
        switch(_currentStage)
        {
            case 1:
                


                break;

            case 2:

            
            break;

        }

    }
}
