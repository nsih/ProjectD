using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class RoomDialogueCon : MonoBehaviour
{
    public GameObject gameManager;

    private static int roomFlag = 0;
    private int currentIndex;


    //
    GameObject talkerInfo;
    GameObject dialogueTxt;
    GameObject niaTxt;

    private bool isTyping = false;



    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        talkerInfo = GameObject.Find("Talker");
        dialogueTxt = GameObject.Find("DialogueText");
        niaTxt = GameObject.Find("NiaText");
    }

    private void Start() 
    {
        StartDialogue();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ProceedNextLine();
        }
        
    }



    #region "dialogue control"
    void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue(roomFlag);
    }

    void ShowDialogue(int currentFlag)
    {
        List<RoomScriptData> currentDialogueData = DialogueData.rsParsedData.FindAll(data => data.flag == currentFlag);
        
        
        if (currentIndex < currentDialogueData.Count)
        {
            RoomScriptData currentLine = currentDialogueData[currentIndex];

            string currentTalker = currentLine.talker;
            string currnetText = currentLine.script;
            float currentTalkSpeed = currentLine.talkSpeed;

            //skip
            if(isTyping == true)
            {
                SkipLine(currentTalker,currnetText);
                return;
            }

            
            
            // 대사 처리
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(currentTalker,currnetText,currentTalkSpeed));
        }
        else
        {
            EndDialogue();
        }
    }

    void ProceedNextLine()
    {
        ShowDialogue(roomFlag);
    }

    void SkipLine(string _currentTalker,string _currentDialogue)
    {
        //Debug.Log("skip");
        StopCoroutine(typingCoroutine);

        if(_currentTalker == "player")
        {
            talkerInfo.GetComponent<TextMeshProUGUI>().text = "player";
            dialogueTxt.GetComponent<TextMeshProUGUI>().text = _currentDialogue;
        }

        else if(_currentTalker == "nia")
        {
            niaTxt.GetComponent<TextMeshProUGUI>().text = _currentDialogue;
        }

        
        isTyping = false;
        currentIndex++;
    }

    void EndDialogue()  //대기화면 시작
    {
        talkerInfo.GetComponent<TextMeshProUGUI>().text = "";
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = "";
        niaTxt.GetComponent<TextMeshProUGUI>().text = "";

        roomFlag++;
    }


    #region "coroutine define"
    private Coroutine typingCoroutine;
    private IEnumerator TypeText(string _currentTalker,string _currentDialogue,float _currentSpeed)
    {
        isTyping = true;
        if(_currentTalker == "player")
        {
            talkerInfo.GetComponent<TextMeshProUGUI>().text = "player";
            dialogueTxt.GetComponent<TextMeshProUGUI>().text = "";

            foreach (char c in _currentDialogue)
            {
                dialogueTxt.GetComponent<TextMeshProUGUI>().text += c;

                //Debug.Log(dialogueTxt.GetComponent<TextMeshProUGUI>().text);

                yield return new WaitForSeconds(_currentSpeed);
            }
            isTyping = false;
            currentIndex++;
            yield break;
        }

        else if(_currentTalker == "nia")
        {
            niaTxt.GetComponent<TextMeshProUGUI>().text = "";

            foreach (char c in _currentDialogue)
            {
                niaTxt.GetComponent<TextMeshProUGUI>().text += c;
                yield return new WaitForSeconds(_currentSpeed);
            }
            isTyping = false;
            currentIndex++;
            yield break;
        }
    }
    #endregion

    #endregion
}

public class RoomScriptData
{
    public int flag;
    public int index;
    public string talker;
    public string script;
    public float talkSpeed;
    public string standImg;

    public RoomScriptData(int flag, int index, string talker, string script, float talkSpeed, string standImg)
    {
        this.flag = flag;
        this.index = index;
        this.talker = talker;
        this.script = script;
        this.talkSpeed = talkSpeed;
        this.standImg = standImg;
    }
}
