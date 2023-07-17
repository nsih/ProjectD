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


    bool isMapOpen;

    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        roomType = GameObject.Find("RoomType");
        phaseType = GameObject.Find("PhaseType");

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


    void MiniMapCon()
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


    void ShowMiniMap()
    {
        GameObject miniMap;

        if(GameManager.currentStage == 0)
        {
            miniMap = pnlBackGround.transform.Find("Stage0MiniMap").gameObject;

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

        if(GameManager.currentStage == 0)
        {
            miniMap = GameObject.Find("Stage0MiniMap");

            isMapOpen = false;
            miniMap.SetActive(false);
        }

        else
        {
            Debug.Log(GameManager.currentStage);
        }
    }
}
