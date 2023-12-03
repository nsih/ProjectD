using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandUICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject landUICanvas;

    GameObject cameraCanvas;


    GameObject stageMap;


    //status UI
    GameObject HPPoolUI;
    GameObject HPPoolPlusUI;
    GameObject actionPointText;
    GameObject sanityBar;
    GameObject coinText;

    GameObject physicalText;
    GameObject mentalText;
    GameObject charmText;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        landUICanvas = GameObject.Find("LandUICanvas");



        cameraCanvas = GameObject.Find("CameraCanvas");

        stageMap = landUICanvas.transform.Find("StageMap").gameObject;



        physicalText = GameObject.Find("Physical");
        mentalText = GameObject.Find("Mental");
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
        mentalText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInfo.mental.ToString();
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
