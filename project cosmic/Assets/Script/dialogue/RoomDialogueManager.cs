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


    GameObject dialogueDataManager;
    GameObject talkerInfo;
    GameObject dialogueTxt;
    GameObject niaTxt;

    Image playerSprite;
    Image niaSprite;

    Color highLight = new Color(0,0,0,255);
    Color shadow = new Color(85,85,85,255);

    private bool isTyping = false;

    private void Start() 
    {
        dialogueDataManager = GameObject.Find("DialogueDataManager");
        //talkerInfo = GameObject.Find("Talker");  지금은 안씀
        dialogueTxt = GameObject.Find("DialogueText"); 
        playerSprite = GameObject.Find("PlayerIMG").GetComponent<Image>();
        niaSprite = GameObject.Find("NiaIMG").GetComponent<Image>();

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
        ShowDialogue("a");  //바꿔줘
    }

    public void ShowDialogue(string _nodeTitle)
    {
        //Debug.Log(DialogueDataManager.roomDialogueData.dialogueData);
        /*
        foreach (var entry in DialogueDataManager.roomDialogueData.dialogueData)
        {
            if(entry.Key == _nodeTitle)
            {
                Debug.Log(entry.Key);
            }
        }
        */
    
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
    private IEnumerator TypeText(string _talker,emotion _emotion,string _text,float _talkSpeed,int _nextLineId)
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
        currentIndex = _nextLineId;
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