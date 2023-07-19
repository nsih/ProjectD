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

    GameObject DoomCounter;


    bool isMapOpen;

    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        roomType = GameObject.Find("RoomType");
        phaseType = GameObject.Find("PhaseType");
        DoomCounter = GameObject.Find("DoomCounter");

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
        DoomCounter.GetComponent<TextMeshProUGUI>().text = GameManager.doomCount.ToString();
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
