using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
public class LeaveRoomBtnCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject roomUICanvas;
    GameObject pnlBackGround;

    GameObject dialogueManager;
    GameObject dialogueOption;



    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        roomUICanvas = GameObject.Find("RoomUICanvas");
        pnlBackGround = GameObject.Find("PnlBackGround");

        dialogueManager = GameObject.Find("DialogueManager");
        dialogueOption = GameObject.Find("DialogueOption");
    }

    public void OnclickLeaveRoom()
    {
        if (!GameManager.isLoading && !GameManager.isRoomTalking)
        {
            dialogueManager.GetComponent<RoomDialogueManager>().ChangeDialogue("LeaveRoomBtnCon");
            dialogueManager.GetComponent<RoomDialogueManager>().StartDialogue();

            dialogueOption.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnClickYes);
            dialogueOption.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(OnClickNo);
        }
    }


    void OnSceneLoaded(AsyncOperation operation)
    {
        if (operation.isDone)
        {
            // ScnLand 씬 로드 후 OpenNewStage 함수를 호출
            gameManager.GetComponent<GameManager>().OpenNewStage();
            gameManager.GetComponent<PlayerInfo>().PlayerStatusInitialize();
        }
    }
    void OnClickYes()
    {
        dialogueOption.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        dialogueOption.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        SceneManager.LoadSceneAsync("ScnLand").completed += OnSceneLoaded;
    }
    void OnClickNo()
    {
        dialogueOption.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        dialogueOption.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
