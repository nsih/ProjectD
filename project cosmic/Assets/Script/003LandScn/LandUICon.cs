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

    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        roomType = GameObject.Find("RoomType");
        phaseType = GameObject.Find("PhaseType");
    }

    // Update is called once per frame
    void Update()
    {
        ShowMiniMap();
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
            phaseType.GetComponent<TextMeshProUGUI>().text = "Action Phase ( "+GameManager.actionStack+ " )";
        }
    }



    void ShowMiniMap()
    {
        GameObject miniMap;

        if(Input.GetKey(KeyCode.Tab))
        {
            if(GameManager.currentStage == 0)
            {
                miniMap = pnlBackGround.transform.Find("Stage0MiniMap").gameObject;

                if(miniMap != null)
                    miniMap.SetActive(true);
            }

            else
            {
                Debug.Log(GameManager.currentStage);
            }
        }





        if(Input.GetKeyUp(KeyCode.Tab))
        {
            if(GameManager.currentStage == 0)
            {
                miniMap = GameObject.Find("Stage0MiniMap");

                miniMap.SetActive(false);
            }

            else
            {
                Debug.Log(GameManager.currentStage);
            }
        }

        
    }
}
