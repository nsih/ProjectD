using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Linq;
using Unity.VisualScripting;


public class RoomDialogueManager : MonoBehaviour
{
    public static bool isRoomTalking = false;

    public static int roomFlag;
    public int currentIndex;

    public Sprite [] playerEmotionSprite;
    public Sprite [] niaEmotionSprite;


    GameObject dialogueDataManager;
    GameObject talkerInfo;
    GameObject dialogueTxt;
    GameObject niaTxt;

    Image playerSprite;
    Image niaSprite;

    Color highLight = new Color(255,255,255,255);
    Color shadow = new Color(85,85,85,255);

    bool isTyping = false;

    string dialogueTitle;


    private void Start() 
    {
        dialogueDataManager = GameObject.Find("DialogueManager");
        //talkerInfo = GameObject.Find("Talker");  안쓰는 UI *Text에 합쳐서 출력하기로함
        dialogueTxt = GameObject.Find("DialogueText"); 
        playerSprite = GameObject.Find("PlayerIMG").GetComponent<Image>();
        niaSprite = GameObject.Find("NiaIMG").GetComponent<Image>();


        dialogueTitle = "start";
        StartDialogue();    //일단 시작하고 써보기.
    }

    private void Update() 
    {
        ProceedNextLine();
    }



    #region "dialogue control"
    public void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue(dialogueTitle);
    }

    public void ShowDialogue(string _nodeTitle)
    {
        DialogueData currentNode = DialogueDataManager.roomDialogueData.FirstOrDefault(entry => entry.title == _nodeTitle);
        
        if(currentNode == null)
        {
            Debug.Log(_nodeTitle + "is not exist");
            return;
        }

        if (currentIndex < currentNode.lines.Length)
        {
            DialogueLine currnetLine = currentNode.lines[currentIndex];

            int currentId = currnetLine.id;
            string currentTalker = currnetLine.talker;
            emotion currentEmotion = dialogueDataManager.GetComponent<DialogueDataManager>().TalkerEmotion(currnetLine.emotion);
            string currentText = currnetLine.text;
            float currentTalkSpeed = dialogueDataManager.GetComponent<DialogueDataManager>().TalkSpeed(currnetLine.talkSpeed);
            bool currentIsLastLing = currnetLine.isLastLine;
            int? currentNextLineId = currnetLine.nextLineId;
            DialogueLineOption[]? currentOption = currnetLine.option;

            //skip
            if(isTyping == true)
            {
                SkipLine(currentTalker,currentText,currentNextLineId);
                return;
            }


            //코루틴 정지
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 다 됐거나 스킵된 이후일것.
            }


            // 대사 처리 (부터합시다 11/02)
            typingCoroutine = 
            StartCoroutine
            (TypeText(currentTalker,currentEmotion,currentText,currentTalkSpeed,currentNextLineId,currentOption));
        }
    
    }

    void ProceedNextLine()
    {
        if (Input.GetKeyDown(KeyCode.E))    //isTalking 추가해야함
        {
            ShowDialogue(dialogueTitle);
        }
    }

    void SkipLine(string _talker,string _text,int? _nextLineId)
    {
        //Debug.Log("skip");
        StopCoroutine(typingCoroutine);

        talkerInfo.GetComponent<TextMeshProUGUI>().text = _talker;
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = _text;
        
        isTyping = false;

        if(_nextLineId == null)
            currentIndex++;
        else
            currentIndex = _nextLineId.Value;
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
    
    private IEnumerator TypeText(string _talker,emotion _emotion,string _text,float _talkSpeed,int? _nextLineId,DialogueLineOption[] _option)
    {
        isTyping = true;

        TalkerHighlightOn(_talker);

        dialogueTxt.GetComponent<TextMeshProUGUI>().text = _talker+" : ";
        

        //토커와 emotion에 따른 이미지 사용
        if(_talker == "player")
            playerSprite.sprite = playerEmotionSprite[(int)_emotion];

        else if(_talker == "nia")
            niaSprite.sprite = niaEmotionSprite[(int)_emotion];
        else
            Debug.Log("talker error : "+_talker);

        foreach (char c in _text)
        {
            dialogueTxt.GetComponent<TextMeshProUGUI>().text += c;

            yield return new WaitForSeconds(_talkSpeed);
        }
        isTyping = false;


        //11.08 여기서 옵션에 따라 UI 표시하고 nextLineId까지 수정해야하는 기능을 넣어야한다 행운을 빈다

        if(_option == null)
        {
            if(_nextLineId == null)
            currentIndex++;
            
            else
                currentIndex = _nextLineId.Value;


            isTyping = false;
        }

        else    //isTyping = false 하지 않는다.
        {
            
        }


        yield break;
    }
    #endregion


    void TalkerHighlightOn(string _talker)
    {
        if(_talker == "player")
        {
            playerSprite.GetComponent<Image>().color = highLight;
            niaSprite.GetComponent<Image>().color = shadow;
        }

        else
        {
            playerSprite.GetComponent<Image>().color = shadow;
            niaSprite.GetComponent<Image>().color = highLight;
        }
    }

    public void TalkerHighlightOFf()
    {
        playerSprite.GetComponent<Image>().color = highLight;
        niaSprite.GetComponent<Image>().color = highLight;
    }
}