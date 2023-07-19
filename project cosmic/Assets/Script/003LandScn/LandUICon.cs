using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandUICon : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject roomType;
    GameObject phaseType;

    GameObject doomCounter;


    GameObject questName;
    GameObject questValue;


    bool isMapOpen;

    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        roomType = GameObject.Find("RoomType");
        phaseType = GameObject.Find("PhaseType");
        doomCounter = GameObject.Find("DoomCounter");

        questName = GameObject.Find("QuestName");
        questValue = GameObject.Find("QuestValue");

        isMapOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            MiniMapCon();
        }


        ShowRoomType();
        ShowPhaseType();
        ShowDoomCounter();
    }


    void ShowDoomCounter()
    {
        doomCounter.GetComponent<TextMeshProUGUI>().text = GameManager.doomCount.ToString();
    }


    void ShowRoomType()
    {
        roomType.GetComponent<TextMeshProUGUI>().text = StageManager.map[GameManager.currentRoom].roomType.ToString();
    }

    void ShowPhaseType()
    {
        if(GameManager.isEncounterPhase)
        {
            phaseType.GetComponent<TextMeshProUGUI>().text = "Encounter Phase";
        }
        else if(GameManager.isActionPhase)
        {
            TextMeshProUGUI textComponent = phaseType.GetComponent<TextMeshProUGUI>();
            if(GameManager.actionStack == 0)
            {
                textComponent.text = "Action Phase ( <color=#FF0000>" + GameManager.actionStack + "</color> )";
            }
            else
            {
                textComponent.text = "Action Phase ( <color=#00e5ff>" + GameManager.actionStack + "</color> )";
            }
            
            phaseType.GetComponent<TextMeshProUGUI>().text = textComponent.text;
        }
    }

    void ShowQuestName()
    {
        if(GameManager.currentStage == 0)
        {
            questName.GetComponent<TextMeshProUGUI>().text = "모든 방을 탐험하기!";
        }

        else if(GameManager.currentStage == 1)
        {
            questName.GetComponent<TextMeshProUGUI>().text = "파멸 카운터를 0으로 만들기";
        }

        else
        {
            Debug.Log("Stage : "+ GameManager.currentStage);
        }
    }

    void ShowQuestValue()
    {
        if(GameManager.currentStage == 0)
        {
            if(GameManager.isQuestDone)
            {
                questValue.GetComponent<TextMeshProUGUI>().text = 
                "탐험한 방 : ("+
                gameObject.GetComponent<StageManager>().CheckisRevealed()+
                " / "+
                StageManager.map.Count+
                ") "+  "완료";
            }
            else
            {
                questValue.GetComponent<TextMeshProUGUI>().text = 
                "탐험한 방 : ("+
                gameObject.GetComponent<StageManager>().CheckisRevealed()+
                " / "+
                StageManager.map.Count+
                ")";
            }
        }

        else if(GameManager.currentStage == 1)
        {
            questValue.GetComponent<TextMeshProUGUI>().text = "파멸 카운터를 0으로 만들기";
        }

        else
        {
            Debug.Log("Stage : "+ GameManager.currentStage);
        }
    }





    public void MiniMapCon()
    {
        if(isMapOpen)
        {
            CloseMiniMap();
        }
        else
        {
            ShowMiniMap();
        }
    }


    public void ShowMiniMap()   
    {
        GameObject miniMap;

        Button closeMapBtn;

        if(GameManager.currentStage == 0)
        {
            miniMap = pnlBackGround.transform.Find("Stage0MiniMap").gameObject;

            closeMapBtn = miniMap.transform.Find("CloseBtn").gameObject.GetComponent<Button>();
            closeMapBtn.onClick.AddListener( CloseMiniMap );

            if(miniMap != null)
            {
                isMapOpen = true;
                miniMap.SetActive(true);
            }
        }

        else
        {
            Debug.Log(GameManager.currentStage);
        }      
    }

    public void CloseMiniMap()
    {
        GameObject miniMap;

        Button closeMapBtn;

        if(GameManager.currentStage == 0)
        {
            miniMap = GameObject.Find("Stage0MiniMap");

            closeMapBtn = miniMap.transform.Find("CloseBtn").gameObject.GetComponent<Button>();
            closeMapBtn.onClick.RemoveAllListeners();


            isMapOpen = false;
            miniMap.SetActive(false);
        }

        else
        {
            Debug.Log(GameManager.currentStage);
        }
    }
}
