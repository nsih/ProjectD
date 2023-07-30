using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LandDialogueManager : MonoBehaviour
{
    public LandDialogueData[] landDialogueDatas;
    public GameObject gameManager;


    GameObject pnlBackGround;
    GameObject dialogueBox;

    GameObject speaker;
    GameObject text;
    GameObject playerCG;
    GameObject opponentCG;


    public int currentDialogueID;
    public int currentIndex;
    bool isTyping = false;
    public bool isRoomTalking = false;

    private bool isSpeaking = false;

    private void Start() 
    {
        gameManager = GameObject.Find("GameManager");
        
        pnlBackGround = GameObject.Find("PnlBackGround");
        dialogueBox = pnlBackGround.transform.Find("DialogueBox").gameObject;

        currentDialogueID = 0;


        StartDialogue();
    }

    void Update ()
    {
        ProceedNextLine();
    }


    public void StartDialogue()
    {
        dialogueBox.SetActive(true);

        playerCG = dialogueBox.transform.Find("PlayerCG").gameObject;
        opponentCG = dialogueBox.transform.Find("OpponentCG").gameObject;
        speaker = dialogueBox.transform.Find("Speaker").gameObject;
        text = dialogueBox.transform.Find("DialogueText").gameObject;

        currentIndex = 0;
        ShowDialogue(currentDialogueID);
    }

    public void ShowDialogue(int _dialogueID)
    {
        if (currentIndex < landDialogueDatas[_dialogueID].dialogues.Length)
        {

            LandDialogueData.DialogueLine currentLandDialogueData = landDialogueDatas[_dialogueID].dialogues[currentIndex];

            LandSpeaker speaker = currentLandDialogueData.speaker;
            string speakerName = currentLandDialogueData.speakerName;
            SpeakSpeed speakSpeed = currentLandDialogueData.speakSpeed;
            string text = currentLandDialogueData.text;
            Emotion playerEmotion = currentLandDialogueData.emotion;
            

            //skipping
            if(isTyping == true)
            {
                SkipLine(speakerName,text);
                return;
            }

            //코루틴 정지
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 다 됐거나 스킵된 이후일것.
            }

            typingCoroutine = StartCoroutine(TypingText(speaker,speakerName,speakSpeed,text,playerEmotion));
        }

        else
        {
            EndDialogue();
        }
    }

    void SkipLine(string _speaker,string _text)
    {
        //Debug.Log("skip");
        StopCoroutine(typingCoroutine);

        text.GetComponent<TextMeshProUGUI>().text = _text;

        isTyping = false;
        currentIndex++;
    }

    void ProceedNextLine()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentIndex != 0 )  //e를 눌렀는데 대화중일때.(index 0아닐때)
        {
            ShowDialogue(currentDialogueID);
        }
    }


    void EndDialogue()  //대기화면 시작
    {
        speaker.GetComponent<TextMeshProUGUI>().text = "";
        text.GetComponent<TextMeshProUGUI>().text = "";

        isRoomTalking = false;
        dialogueBox.SetActive(false);

    }





    /////

    private Coroutine typingCoroutine;
    private IEnumerator TypingText( LandSpeaker _speaker ,string _speakerName, SpeakSpeed _speakSpeed, string _text,
                                    Emotion _emotion)
    {
        isRoomTalking = true;
        isTyping = true;

        speaker.GetComponent<TextMeshProUGUI>().text = _speakerName;

        text.GetComponent<TextMeshProUGUI>().text = "";


        getStandingCG(_speaker,_emotion);   //CG없어서 비워둠


        foreach (char c in _text)
        {
            text.GetComponent<TextMeshProUGUI>().text += c;

            yield return new WaitForSeconds(GetSpeakSpeed(_speakSpeed));
        }
        isTyping = false;
        currentIndex++;
        yield break;
    }


    void getStandingCG(LandSpeaker _speaker,Emotion _emotion)
    {
        if(_speaker == LandSpeaker.Player)
        {
            //플레이어 cg만 활성화
        }
        else
        {
            //상대 cg만 활성화
        }

    }


    float GetSpeakSpeed(SpeakSpeed speakSpeed)  //이건 번역 아니라 괜찮아
    {
        if(speakSpeed == SpeakSpeed.slow)
            return 0.2f;
        else if(speakSpeed == SpeakSpeed.normal)
            return 0.07f;
        else if(speakSpeed == SpeakSpeed.fast)
            return 0.03f;

        return 0;
    }



}
