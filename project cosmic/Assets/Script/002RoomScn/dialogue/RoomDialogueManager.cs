using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomDialogueManager : MonoBehaviour
{
    public RoomDialogueData[] roomDialogueDatas;
    public GameObject gameManager;


    GameObject speaker;
    GameObject text;
    GameObject playerCG;
    GameObject niaCG;


    public int currentDialogueID;
    public int currentIndex;
    bool isTyping = false;
    public bool isRoomTalking = false;

    //private bool isSpeaking = false;

    private void Start() 
    {
        playerCG = GameObject.Find("PlayerCG");
        niaCG = GameObject.Find("NiaCG");

        gameManager = GameObject.Find("GameManager");
        speaker = GameObject.Find("Speaker");
        text = GameObject.Find("DialogueText");

        currentDialogueID = 0;


        StartDialogue();
    }

    void Update ()
    {
        ProceedNextLine();
    }


    public void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue(currentDialogueID);
    }

    public void ShowDialogue(int _dialogueID)
    {
        if (currentIndex < roomDialogueDatas[_dialogueID].dialogues.Length)
        {
            RoomDialogueData.DialogueLine currentRoomDialogueData = roomDialogueDatas[_dialogueID].dialogues[currentIndex];

            Speaker speaker = currentRoomDialogueData.speaker;
            string speakerName = currentRoomDialogueData.speakerName;
            SpeakSpeed speakSpeed = currentRoomDialogueData.speakSpeed;
            string text = currentRoomDialogueData.text;
            Emotion playerEmotion = currentRoomDialogueData.playerEmotion;
            Emotion niaEmotion = currentRoomDialogueData.niaEmotion;
            

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

            typingCoroutine = StartCoroutine(TypingText(speaker,speakerName,speakSpeed,text,playerEmotion,niaEmotion));
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
    }





    /////

    private Coroutine typingCoroutine;
    private IEnumerator TypingText( Speaker _speaker ,string _speakerName, SpeakSpeed _speakSpeed, string _text,
                                    Emotion _playerEmotion,Emotion _niaEmotion)
    {
        isRoomTalking = true;
        isTyping = true;

        speaker.GetComponent<TextMeshProUGUI>().text = _speakerName;

        text.GetComponent<TextMeshProUGUI>().text = "";

        //playerCG.GetComponent<Image>().sprite == 
        //niaCG.GetComponent<Image>().sprite == 

        foreach (char c in _text)
        {
            text.GetComponent<TextMeshProUGUI>().text += c;

            yield return new WaitForSeconds(GetSpeakSpeed(_speakSpeed));
        }
        isTyping = false;
        currentIndex++;
        yield break;
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
