using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using JetBrains.Annotations;


public class RoomDialogueManager : MonoBehaviour
{
    public static bool isRoomTalking = false;
    bool isChoosingOption = false;

    public static int roomFlag;

    public bool isCurrentLineEnd = true;
    public int currentIndex;

    public Sprite [] playerEmotionSprite;
    public Sprite [] niaEmotionSprite;


    GameObject gameManager;
    GameObject dialogueDataManager;
    GameObject talkerInfo;
    GameObject dialogueTxt;
    GameObject niaTxt;
    GameObject dialogueOption;



    List<DialogueData> roomChitChatNodeList;





    Image playerSprite;
    Image niaSprite;

    Color highLight = new Color(1,1,1,1);
    Color shadow = new Color(0.33f,0.33f,0.33f,1);

    bool isTyping = false;


    
    string dialogueTitle;


    private void Start() 
    {
        gameManager = GameObject.Find("GameManager");
        dialogueDataManager = GameObject.Find("DialogueManager");
        //talkerInfo = GameObject.Find("Talker");   현재 안씀
        dialogueTxt = GameObject.Find("DialogueText"); 
        dialogueOption = GameObject.Find("DialogueOption");
        playerSprite = GameObject.Find("PlayerIMG").GetComponent<Image>();
        niaSprite = GameObject.Find("NiaIMG").GetComponent<Image>();


        //잡담 대화 노드리스트 초기화
        roomChitChatNodeList = DialogueDataManager.roomDialogueData.Where(entry => entry.title.Contains("chitchat")).ToList();


        StartDialogue();    //일단 시작하고 써보기.
    }

    private void Update() 
    {
        ProceedNextLine(dialogueTitle);

        //Debug.Log(dialogueTitle);
    }



    #region "dialogue control"
    public void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        isRoomTalking = true;

        //
        if(dialogueTitle == null)
        {
            dialogueTitle = "start";
            //Debug.Log("node Title : "+dialogueTitle);
        }

        //타이틀에 맞는 노드 차출.
        DialogueData currentNode = new DialogueData();
        if(dialogueTitle == "chitchat")
        {
            int randomIndex = UnityEngine.Random.Range(0, roomChitChatNodeList.Count);

            currentNode = roomChitChatNodeList[randomIndex];
        }
        else
        {
            currentNode = DialogueDataManager.roomDialogueData.FirstOrDefault(entry => entry.title == dialogueTitle);
        }



        
        if(currentNode == null)
        {
            Debug.Log(dialogueTitle + "is not exist");
            return;
        }

        

        //시작라인 이거나, 진행중인 라인이거나, 끝까지 진행한 라인
        if (currentIndex == 0 || isCurrentLineEnd == false)
        {
            isCurrentLineEnd = false;   //대화 시작시 false 전환

            DialogueLine currnetLine = currentNode.lines[currentIndex];

            int currentId = currnetLine.id;
            string currentTalker = currnetLine.talker;
            emotion currentEmotion = dialogueDataManager.GetComponent<DialogueDataManager>().TalkerEmotion(currnetLine.emotion);
            string currentText = currnetLine.text;
            float currentTalkSpeed = dialogueDataManager.GetComponent<DialogueDataManager>().TalkSpeed(currnetLine.talkSpeed);
            bool currentIsLastLine = currnetLine.isLastLine;
            int? currentNextLineId = currnetLine.nextLineId;
            DialogueLineOption[] currentOption = currnetLine.option;

            //타이핑중에 한번더 호출하면 타이핑 스킵
            if(isTyping == true )
            {
                //선택중이면 반려
                if(isChoosingOption)
                    return;
                
                //아니면 스킵
                SkipLine(currentTalker,currentText,currentNextLineId,currentOption,currentIsLastLine);
                return;
            }

            //코루틴 정지
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 다 됐거나 스킵된 이후일것.
            }


            // 대사 처리
            typingCoroutine = StartCoroutine(TypeText(currentTalker,currentEmotion,currentText,currentTalkSpeed,currentNextLineId,currentOption,currentIsLastLine));
        }

        //노드의 라인 종료
        else
        {
            EndDialogue();
        }
    
    }

    void ProceedNextLine(string _nodeTitle)
    {
        if (Input.GetKeyDown(KeyCode.E) && isRoomTalking)    //토킹중에 e 키 누르면
        {
            ShowDialogue();
        }
    }

    void SkipLine(string _talker,string _text,int? _nextLineId,DialogueLineOption[] _option,bool _currentIsLastLine)
    {
        //Debug.Log("skip");
        StopCoroutine(typingCoroutine);

        //talkerInfo.GetComponent<TextMeshProUGUI>().text = _talker;
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = _talker + " : " + _text;
        
        isTyping = false;

        OptionProcessing(_option,_nextLineId);
        //타이핑 완료시 end 체크
        if(!isTyping)
        {
            isCurrentLineEnd = _currentIsLastLine;
        }
    }

    void EndDialogue()  //대기화면 시작
    {
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = "";
        isRoomTalking = false;
        TalkerHighlightOff();
    }
    #endregion


    #region "coroutine define"
    private Coroutine typingCoroutine;
    
    private IEnumerator TypeText(string _talker,emotion _emotion,string _text,float _talkSpeed,int? _nextLineId,DialogueLineOption[] _option,bool _currentIsLastLine)
    {
        isTyping = true;    //타이핑 중.

        TalkerHighlightOn(_talker); //토커에 따른 이미지 하이라이트 처리

        //이름부터 출력
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = _talker+" : ";
        

        //토커와 emotion에 따른 이미지 처리
        if(_talker == "player")
            playerSprite.sprite = playerEmotionSprite[(int)_emotion];

        else if(_talker == "nia")
            niaSprite.sprite = niaEmotionSprite[(int)_emotion];
        else
            Debug.Log("talker error : "+_talker);


        //타이핑 처리
        foreach (char c in _text)
        {
            dialogueTxt.GetComponent<TextMeshProUGUI>().text += c;

            gameManager.GetComponent<SFXManager>().PlaySound(SfxType.DialogueTyping);

            yield return new WaitForSeconds(_talkSpeed);
        }
        isTyping = false;


        //종료처리 (옵션)
        OptionProcessing(_option,_nextLineId);

        //타이핑 완료시 end 체크
        if(!isTyping)
        {
            isCurrentLineEnd = _currentIsLastLine;
        }
        yield break;
    }
    #endregion

    void OptionProcessing(DialogueLineOption[] _option, int? _nextLineId)
    {
        //종료처리 (옵션)

        //옵션이 없을 경우 종료
        if(_option == null)
        {
            if(_nextLineId == null)
            {
                currentIndex++;
            }
            
            else
            {
                currentIndex = _nextLineId.Value;
            }
        }

        //옵션이 있을경우
        else
        {
            isTyping = true;
            isChoosingOption = true;

            GameObject option1 = dialogueOption.transform.GetChild(0).gameObject;
            GameObject option2 = dialogueOption.transform.GetChild(1).gameObject;

            option1.SetActive(true);
            option2.SetActive(true);

            option1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = _option[0].text;
            option2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = _option[1].text;

            option1.GetComponent<Button>().onClick.AddListener(() => OptionClicked(_option[0]));
            option2.GetComponent<Button>().onClick.AddListener(() => OptionClicked(_option[1]));
        }

    }

    void OptionClicked(DialogueLineOption dialogueLineOption)
    {
        //해당 라인 끝
        isTyping = false;

        //옵션 UI 비활성화
        dialogueOption.transform.GetChild(0).gameObject.SetActive(false);
        dialogueOption.transform.GetChild(1).gameObject.SetActive(false);

        isChoosingOption = false;


        //마지막 라인 - 종료
        if(dialogueLineOption.isLastLine)
        {
            EndDialogue();
        }

        //!마지막 라인 - index 수정후 다시 호출
        else
        {
            currentIndex = dialogueLineOption.nextLineId.Value;

            ShowDialogue();
        }
        
    }


    void TalkerHighlightOn(string _talker)
    {
        if(_talker == "player")
        {
            playerSprite.GetComponent<Image>().color = highLight;
            niaSprite.GetComponent<Image>().color = shadow;
        }

        else if(_talker == "nia")
        {
            playerSprite.GetComponent<Image>().color = shadow;
            niaSprite.GetComponent<Image>().color = highLight;
        }

        else
        {
            Debug.Log(_talker);
        }

        // Debug.Log(playerSprite.GetComponent<Image>().color);
        // Debug.Log(niaSprite.GetComponent<Image>().color);
    }

    public void TalkerHighlightOff()
    {
        playerSprite.GetComponent<Image>().color = highLight;
        niaSprite.GetComponent<Image>().color = highLight;
    }


    //title 변경
    public void ChangeDialogue(string changeDialogueTitle)
    {
        dialogueTitle = changeDialogueTitle;
    }

    //타이틀에 맞는 대화 노드 리스트 초기화 (다른 리스트 필요하면 쓰는걸로)
    public void InitializeDialogueNodeList()
    {
        //잡담 리스트
        roomChitChatNodeList = DialogueDataManager.roomDialogueData.Where(entry => entry.title.Contains("chitchat")).ToList();
    }
}