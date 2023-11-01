using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class RoomDialogueManager : MonoBehaviour
{
    public static bool isRoomTalking = false;

    public static int roomFlag;
    public int currentIndex;


    //
    GameObject talkerInfo;
    GameObject dialogueTxt;
    GameObject niaTxt;

    private bool isTyping = false;



    private void Awake()
    {
        talkerInfo = GameObject.Find("Talker");
        dialogueTxt = GameObject.Find("DialogueText");
    }

    private void Start() 
    {
        StartDialogue();
    }

    private void Update() 
    {
        ProceedNextLine();
    }



    #region "dialogue control"
    public void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue("roomFlag");
    }

    public void ShowDialogue(string _nodeName)
    {
        string nodeName = _nodeName;
        List<RoomDialogueNode> currentNode;

        if (DialogueDataManager.roomDialogueData.node.ContainsKey(_nodeName))
        {
            currentNode = DialogueDataManager.roomDialogueData.node[_nodeName];
        }
        else
        {
            Debug.Log(_nodeName+"이 없습니다.");

            return;
        }
        
        
        if (currentIndex < currentNode.Count)
        {
            RoomDialogueNode currentLine = currentNode[currentIndex];

            string currentTalker = currentLine.talker;
            string currnetText = currentLine.text;
            string currentTalkSpeed = currentLine.talkSpeed;

            //skip
            if(isTyping == true)
            {
                SkipLine(currentTalker,currnetText);
                return;
            }

            //코루틴 정지
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 다 됐거나 스킵된 이후일것.
            }

            // 대사 처리 (부터합시다 11/02)
            typingCoroutine = StartCoroutine(TypeText(currentTalker,currnetText,1));
        }
        else
        {
            EndDialogue();
        }
    }

    void ProceedNextLine()
    {
        if (Input.GetKeyDown(KeyCode.E))    //대화중인 조건 추가해야함
        {
            //ShowDialogue(roomFlag);
        }
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

        //roomFlag++;   플레그 자동 조종도 좋지만. 그냥 일일히 이벤트에 따라 플레그 설정하는것도 고려중.
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
