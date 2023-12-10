using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandUICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject landUICanvas;


    GameObject stageMap;


    //status UI
    public GameObject[] maxHPUI = new GameObject[15];
    public GameObject[] currentHPUI = new GameObject[15];

    GameObject apUI;
    GameObject coinUI;

    GameObject physicalUI;
    GameObject mentalUI;
    GameObject charmUI;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        landUICanvas = GameObject.Find("LandUICanvas");


        stageMap = landUICanvas.transform.Find("StageMap").gameObject;

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

    // Update is called once per frame
    void Update()
    {
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
        physicalUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.physical.ToString();
        mentalUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.mental.ToString();
        charmUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.charm.ToString();
    }
    #endregion


    #region "Map"
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
        //Button closeMapBtn;

        //closeMapBtn = stageMap.transform.Find("CloseBtn").gameObject.GetComponent<Button>();
        //closeMapBtn.onClick.AddListener( CloseStageMap );
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
}
