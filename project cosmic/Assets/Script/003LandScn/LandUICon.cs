using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandUICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject pnlBackGround;
    GameObject phaseType;


    GameObject cameraCanvas;
    GameObject roomType;
    GameObject mentalityCounter;
    GameObject questTitle;
    GameObject questContent;
    GameObject questDetail;

    GameObject roomIntroPanel;


    public Sprite[] roomTypePanelImg = new Sprite[7];


    bool isMapOpen;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        pnlBackGround = GameObject.Find("PnlBackGround");
        phaseType = GameObject.Find("PhaseType");
        mentalityCounter = GameObject.Find("MentalityCounter");

        cameraCanvas = GameObject.Find("CameraCanvas");
        roomType = GameObject.Find("RoomType");

        questTitle = GameObject.Find("QuestTitle");
        questContent = GameObject.Find("QuestContent");
        questDetail = GameObject.Find("QuestDetail");

        roomIntroPanel  = pnlBackGround.transform.Find("RoomIntroPanel").gameObject;

        isMapOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.isLoading && !roomIntroPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                MiniMapCon();
            }


            ShowRoomType();
            ShowPhaseType();
            ShowMentalCounter();

            ShowQuestName();
            ShowQuestValue();
        }
    }


    void ShowMentalCounter()
    {
        mentalityCounter.GetComponent<TextMeshProUGUI>().text = GameManager.mentality.ToString();
    }


    void ShowRoomType()
    {
        roomType.GetComponent<TextMeshProUGUI>().text = StageManager.map[GameManager.currentRoom].roomType.ToString();
    }

    void ShowPhaseType()
    {
        if(!GameManager.isActionPhase)
        {
            phaseType.GetComponent<TextMeshProUGUI>().text = "행동단계가 아닙니다.";
        }
        else if(GameManager.isActionPhase)
        {
            TextMeshProUGUI textComponent = phaseType.GetComponent<TextMeshProUGUI>();
            if(GameManager.actionStack == 0)
            {
                textComponent.text = "행동단계! ( <color=#FF0000>" + GameManager.actionStack + "</color> )";
            }
            else
            {
                textComponent.text = "행동단계! ( <color=#00e5ff>" + GameManager.actionStack + "</color> )";
            }
            
            phaseType.GetComponent<TextMeshProUGUI>().text = textComponent.text;
        }
    }

    void ShowQuestName()
    {
        questTitle.GetComponent<TextMeshProUGUI>().text = "Stage "+GameManager.currentStage;
        if(GameManager.currentStage == 0)
        {
            questContent.GetComponent<TextMeshProUGUI>().text = "모든 방을 탐험하기!";
        }

        else if(GameManager.currentStage == 1)
        {
            questContent.GetComponent<TextMeshProUGUI>().text = "파멸 카운터를 0으로 만들기";
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
                questDetail.GetComponent<TextMeshProUGUI>().text = 
                "탐험한 방\n("+
                gameManager.GetComponent<StageManager>().CheckisRevealed()+
                " / "+
                StageManager.map.Count+
                ")    "+  
                "<color=#5882FA>완료!</color>";
            }
            else
            {
                questDetail.GetComponent<TextMeshProUGUI>().text = 
                "탐험한 방\n(" +
                gameManager.GetComponent<StageManager>().CheckisRevealed() +
                " / " +
                StageManager.map.Count +
                ")    " + 
                "<color=#FA5858>진행중</color>";
            }
        }

        else if(GameManager.currentStage == 1)
        {
            questDetail.GetComponent<TextMeshProUGUI>().text = "파멸 카운터를 0으로 만들기";
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

            if(miniMap != null && !GameManager.isLoading)
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

    public void StartShowRoomIntroPanel()
    {
        StartCoroutine("ShowRoomIntroPanel");
    }

    IEnumerator ShowRoomIntroPanel()
    { 
        roomIntroPanel.SetActive(true);

        if(StageManager.map[ GameManager.currentRoom ].roomType == RoomType.Null)
        {
            roomIntroPanel.GetComponent<Image>().sprite = roomTypePanelImg[0];
        }

        else if(StageManager.map[ GameManager.currentRoom ].roomType == RoomType.Altar)
        {
            roomIntroPanel.GetComponent<Image>().sprite = roomTypePanelImg[1];
        }

        else if(StageManager.map[ GameManager.currentRoom ].roomType == RoomType.Battle)
        {
            roomIntroPanel.GetComponent<Image>().sprite = roomTypePanelImg[2];
        }

        else if(StageManager.map[ GameManager.currentRoom ].roomType == RoomType.Shop)
        {
            roomIntroPanel.GetComponent<Image>().sprite = roomTypePanelImg[3];
        }

        else if(StageManager.map[ GameManager.currentRoom ].roomType == RoomType.Test)
        {
            roomIntroPanel.GetComponent<Image>().sprite = roomTypePanelImg[4];
        }

        else
        {
            roomIntroPanel.GetComponent<Image>().sprite = roomTypePanelImg[5];
        }

        gameManager.GetComponent<GameManager>().PauseGame();





        while (!Input.GetMouseButtonDown(0))
        {
            gameManager.GetComponent<GameManager>().ResumeGame();
            yield return null; // 다음 프레임까지 기다림
        }

        roomIntroPanel.SetActive(false);        
    }

    public bool isRoomIntroPanel()
    {
        return roomIntroPanel.activeSelf;
    }
}
