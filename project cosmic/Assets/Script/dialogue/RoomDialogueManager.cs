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

    public Sprite [] playerEmotionSprite;
    public Sprite [] niaEmotionSprite;


    //
    GameObject dialogueDataManager;
    GameObject talkerInfo;
    GameObject dialogueTxt;
    GameObject niaTxt;

    Sprite playerSprite;
    Sprite niaSprite;

    private bool isTyping = false;

    private void Start() 
    {
        dialogueDataManager = GameObject.Find("DialogueDataManager");
        //talkerInfo = GameObject.Find("Talker");  지금은 안씀
        dialogueTxt = GameObject.Find("DialogueText"); 
        playerSprite = GameObject.Find("PlayerIMG").GetComponent<Image>().sprite;
        niaSprite = GameObject.Find("NiaIMG").GetComponent<Image>().sprite;

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

            string talker = currentLine.talker;
            string emotion = currentLine.emotion;
            string text = currentLine.text;
            float talkSpeed = dialogueDataManager.GetComponent<DialogueDataManager>().TalkSpeed(currentLine.talkSpeed);
            int nextLineId = int.Parse(currentLine.nextLineId);

            //skip
            if(isTyping == true)
            {
                SkipLine(talker,text,nextLineId);
                return;
            }

            //코루틴 정지
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 다 됐거나 스킵된 이후일것.
            }

            // 대사 처리 (부터합시다 11/02)
            typingCoroutine = StartCoroutine(TypeText(talker,text,talkSpeed));
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

    void SkipLine(string _talker,string _text,int _nextLineId)
    {
        //Debug.Log("skip");
        StopCoroutine(typingCoroutine);

        talkerInfo.GetComponent<TextMeshProUGUI>().text = _talker;
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = _text;
        
        isTyping = false;
        currentIndex = _nextLineId;
    }

    void EndDialogue()  //대기화면 시작
    {
        talkerInfo.GetComponent<TextMeshProUGUI>().text = "";
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = "";
        niaTxt.GetComponent<TextMeshProUGUI>().text = "";

        //roomFlag++;   플레그 자동 조종도 좋지만. 그냥 일일히 이벤트에 따라 플레그 설정하는것도 고려중.
    }
    #endregion


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
}