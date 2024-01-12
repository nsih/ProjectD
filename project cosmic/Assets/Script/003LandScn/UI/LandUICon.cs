using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandUICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject landUICanvas;

    //move (map)
    GameObject stageMap;

    //item UI
    GameObject itemPopup;

    //action UI
    GameObject actionPopup;
    GameObject actionScrollView;
    GameObject actionViewport;
    GameObject actionScrollContent;



    public GameObject actionContainer;


    //status UI
    public GameObject[] maxHPUI = new GameObject[15];
    public GameObject[] currentHPUI = new GameObject[15];

    GameObject apUI;
    GameObject coinUI;

    GameObject physicalUI;
    GameObject mentalUI;
    GameObject charmUI;


    //sprite data
    public Sprite physicalIcon;
    public Sprite mentalIcon;
    public Sprite charmIcon;
    public Sprite RandomIcon;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        landUICanvas = GameObject.Find("LandUICanvas");

        //map
        stageMap = landUICanvas.transform.Find("StageMap").gameObject;

        //actions
        actionPopup = landUICanvas.transform.Find("ActionPopup").gameObject;
        actionScrollView = actionPopup.transform.Find("Scroll View").gameObject;
        actionViewport = actionScrollView.transform.Find("Viewport").gameObject;
        actionScrollContent = actionViewport.transform.Find("Content").gameObject;


        //status
        //HP
        for (int i = 0; i < 15; i++)
        {
            maxHPUI[i] = GameObject.Find("MaxHP").transform.GetChild(i).gameObject;
            currentHPUI[i] = GameObject.Find("CurrentHP").transform.GetChild(i).gameObject;
        }

        //AP
        apUI = GameObject.Find("APText");

        //coin
        coinUI = GameObject.Find("CoinText");

        //status
        physicalUI = GameObject.Find("Physical");
        mentalUI = GameObject.Find("Mental");
        charmUI = GameObject.Find("Charm");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (actionPopup.activeSelf)
            {
                ActionListSwitch();
            }

            //
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StageMapSwitch();
        }
    }


    #region "update UI data"
    public void UpdateHPUI()
    {
        int currentHP = PlayerInfo.currentHP;
        int maxHP = PlayerInfo.maxHp;


        for (int i = 0; i < maxHPUI.Length; i++)
        {
            if (i < maxHP)
            {
                maxHPUI[i].SetActive(true);
            }
            else
            {
                maxHPUI[i].SetActive(false);
            }

            if (i < currentHP)
            {
                currentHPUI[i].SetActive(true);
            }
            else
            {
                currentHPUI[i].SetActive(false);
            }
        }
    }

    public void UpdateAPUI()
    {
        int currentAP = PlayerInfo.currentAP;
        int maxAP = PlayerInfo.maxAP;

        apUI.GetComponent<TMP_Text>().text = currentAP + " / " + maxAP;
    }

    public void UpdateCoinText()
    {
        int coin = PlayerInfo.coin;

        coinUI.GetComponent<TMP_Text>().text = coin.ToString();
    }

    public void UpdateStatusText()
    {
        physicalUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
        = PlayerInfo.physical.ToString() + "<color=black>" + "\n(" + PlayerInfo.diceP.ToString() + ")" + "</color>";

        mentalUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
        = PlayerInfo.mental.ToString() + "<color=black>" + "\n(" + PlayerInfo.diceM.ToString() + ")" + "</color>";

        charmUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
        = PlayerInfo.charm.ToString() + "<color=black>" + "\n(" + PlayerInfo.diceC.ToString() + ")" + "</color>";
    }
    #endregion


    #region  "Item List UI"
    void ShowItemUI()
    {

    }

    void CloseItemUI()
    {

    }

    public void ItemListSwitch()
    {

    }


    #endregion

    #region "Action ScrollView UI"
    void ShowActionScroll()
    {
        actionPopup.SetActive(true);
    }
    void CloseActionScroll()
    {
        actionPopup.SetActive(false);
    }
    public void ActionListSwitch()
    {
        if (actionPopup.activeSelf)
        {
            ClearActionScroll();
            CloseActionScroll();
        }
        else
        {
            PopulateActionScroll();
            ShowActionScroll();
        }
    }

    //action scroll UI 초기화(with open)
    void PopulateActionScroll()
    {
        //int instanceIndex = 0;

        RectTransform containerRectTransform = actionContainer.GetComponent<RectTransform>();
        float containerHeight = containerRectTransform.sizeDelta.y;

        float currentY = 0;


        //content size adjust
        RectTransform contentRectTransform = actionScrollContent.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta =
        new Vector2(contentRectTransform.sizeDelta.x, PlayerInfo.playerActionList.Count * (containerHeight + 50));


        //button instance
        for (int i = 0; i < PlayerInfo.playerActionList.Count; i++)
        {
            int instanceIndex = i;//람다 closure용

            // prefap instance 배치
            GameObject _actionContainer = Instantiate(actionContainer, actionViewport.transform);

            // instance initialize
            _actionContainer.transform.Find("Icon").GetComponent<Image>().sprite = PlayerInfo.playerActionList[i].icon;
            _actionContainer.transform.Find("Name").GetComponent<TMP_Text>().text = PlayerInfo.playerActionList[i].actionName;
            _actionContainer.transform.Find("TestTypeIMG").GetComponent<Image>().sprite = GetTestTypeIMG(PlayerInfo.playerActionList[i].testType);
            _actionContainer.transform.Find("TestOffset").GetComponent<TMP_Text>().text = PlayerInfo.playerActionList[i].testOffset.ToString();
            _actionContainer.transform.Find("Cost").GetComponent<TMP_Text>().text = PlayerInfo.playerActionList[i].cost.ToString();
            _actionContainer.transform.Find("Comment").GetComponent<TMP_Text>().text = PlayerInfo.playerActionList[i].actionText;


            //add listner test event manager
            _actionContainer.GetComponent<Button>().onClick.AddListener(() =>
            {
                gameManager.GetComponent<ActionManager>().StartActionTestEvent(instanceIndex);
            });

            //instance container의 Y position 설정
            RectTransform uiRectTransform = _actionContainer.GetComponent<RectTransform>();
            if (uiRectTransform != null)
            {
                uiRectTransform.anchoredPosition = new Vector2(0f, -currentY + 365);

                _actionContainer.transform.SetParent(actionScrollContent.transform, true);

                currentY += containerHeight + 25;
            }

            //
            //PlayerInfo.playerActionList[i].isUsed = true;
        }


        Sprite GetTestTypeIMG(TestType testType)
        {
            if (testType == TestType.Physical)
            {
                return physicalIcon;
            }
            else if (testType == TestType.Mental)
            {
                return mentalIcon;
            }
            else if (testType == TestType.Charm)
            {
                return charmIcon;
            }
            else if (testType == TestType.Random)
            {
                return RandomIcon;
            }
            else if (testType == TestType.None)
            {
                return null;
            }
            else
            {
                return null;
                //Debug.Log("test type error");
            }
        }
    }

    //action scroll UI 초기화(with close)
    void ClearActionScroll()
    {
        foreach (Transform child in actionScrollContent.gameObject.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }

        // actionScrollContent size 조절
        RectTransform contentRectTransform = actionScrollContent.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, 0f);

    }
    #endregion


    #region "Move(Map) UI"
    public void StageMapSwitch()
    {
        if (stageMap.activeSelf)
        {
            CloseStageMap();
        }
        else
        {
            ShowStageMap();
        }
    }
    public void ShowStageMap()
    {
        stageMap.SetActive(true);
    }
    public void CloseStageMap()
    {
        //Button closeMapBtn;

        //closeMapBtn = stageMap.transform.Find("CloseBtn").gameObject.GetComponent<Button>();
        //closeMapBtn.onClick.RemoveAllListeners();
        stageMap.SetActive(false);
    }
    #endregion
    //
}
