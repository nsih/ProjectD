using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandUICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject pnlBackGround;

    GameObject cameraCanvas;


    GameObject stageMap;


    //status UI
    GameObject HPPoolUI;
    GameObject HPPoolPlusUI;
    GameObject actionPointText;
    GameObject sanityBar;
    GameObject coinText;

    GameObject physicalText;
    GameObject willPowerText;
    GameObject knowledgeText;
    GameObject charmText;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        pnlBackGround = GameObject.Find("PnlBackGround");



        cameraCanvas = GameObject.Find("CameraCanvas");

        stageMap = pnlBackGround.transform.Find("StageMap").gameObject;



        physicalText = GameObject.Find("Physical");
        willPowerText = GameObject.Find("WillPower");
        knowledgeText = GameObject.Find("Knowledge");
        charmText = GameObject.Find("Charm");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            StageMapSwitch();
        }
    }


    //update UI data
    void UpdateHPUI()
    {

    }

    void UpdateAPText()
    {

    }

    void UpdateCoinText()
    {

    }

    void UpdateSanityBar()
    {
        //sanityBar.GetComponent<TextMeshProUGUI>().text = PlayerInfo.sanity.ToString();
    }

    void UpdateStatusText()
    {
        physicalText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.physical.ToString();
        willPowerText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.willPower.ToString();
        knowledgeText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.knowledge.ToString();
        charmText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.charm.ToString();
    }


    void UpdateBtnMoveText()
    {

    }

    void UpdateBtnRecoverText()
    {
        
    }

    void UpdateBtnActionText()
    {
        
    }

    void UpdateBtnArtifactText()
    {
        
    }



    /// <summary>
    /// map control
    /// </summary>


    #region "Map"
    public void StageMapSwitch()
    {
        if(stageMap.activeSelf)
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
