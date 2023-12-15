using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ActionBtnCon : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject actionContainer;

    public Sprite[] sprites = new Sprite[4];

    GameObject gameManager;
    GameObject landUiCanvas;
    GameObject actionPopup;
    GameObject ScrollView;
    GameObject ScrollContent;


    GameObject ButtonText;
    GameObject ButtonImage;



    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        landUiCanvas = GameObject.Find("LandUICanvas");
        actionPopup = landUiCanvas.transform.Find("ActionPopup").gameObject;
        
        ScrollView = actionPopup.transform.Find("Scroll View").gameObject;
        ScrollContent = ScrollView.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject;


        ButtonText = this.gameObject.transform.GetChild(0).gameObject;
        ButtonImage = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update ()
    {

    }


    public void OnClickAction()
    {
        OpenPopup();
    }

    void OpenPopup()
    {
        actionPopup.SetActive(true);

        //btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        actionPopup.SetActive(false);
    }


    //

    void PopulateScrollView()
    {
        foreach (var actionData in PlayerInfo.playerActionList)
        {
            // UI 프리팹을 인스턴스화하여 content 아래에 배치
            GameObject _actionContainer = Instantiate(actionContainer, ScrollContent.transform);
            
            // 생성된 UI에 데이터를 적용
            _actionContainer.transform.Find("Icon").GetComponent<Image>().sprite = actionData.icon;
            _actionContainer.transform.Find("TestTypeIMG").GetComponent<Image>().sprite = GetTestTypeIMG(actionData.testType);
            _actionContainer.transform.Find("Name").GetComponent<TMP_Text>().text = actionData.name;
            _actionContainer.transform.Find("Cost").GetComponent<TMP_Text>().text = "COST " + (int)actionData.cost;
            _actionContainer.transform.Find("Comment").GetComponent<TMP_Text>().text = actionData.afterComment;

            //_actionContainer.GetComponent<Button>().onClick.AddListener();
            //gameManager.GetComponent<ActionManager>().
            




        }
    }

    Sprite GetTestTypeIMG(TestType testType)
    {
        if(testType == TestType.Physical)
        {
            return sprites[0];
        }
        else if(testType == TestType.Mental)
        {
            return sprites[1];
        }
        else if(testType == TestType.Charm)
        {
            return sprites[2];
        }
        else if(testType == TestType.Random)
        {
            return sprites[3];
        }
        else if(testType == TestType.None)
        {
            return sprites[4];
        }
        else
        {
            return sprites[4];
            Debug.Log("test type error");
        }
    }
}

