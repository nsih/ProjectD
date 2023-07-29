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


    public static int dialogueID = 0;
    public int currentIndex;
    bool isTyping = false;

    private bool isSpeaking = false;

    private void Start() 
    {
        playerCG = GameObject.Find("PlayerCG");
        niaCG = GameObject.Find("NiaCG");

        gameManager = GameObject.Find("GameManager");
        speaker = GameObject.Find("Talker");
        text = GameObject.Find("DialogueText");
    }


    public void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue(dialogueID);
    }

    void ShowDialogue(int _dialogueID)
    {
        RoomDialogueData.DialogueLine currentRoomDialogueData = roomDialogueDatas[_dialogueID].dialogues[currentIndex];

        Speaker speaker = currentRoomDialogueData.speaker;
        string speakerName = currentRoomDialogueData.speakerName;
        SpeakSpeed speakSpeed = currentRoomDialogueData.speakSpeed;
        string text = currentRoomDialogueData.text;
        Emotion playerEmotion = currentRoomDialogueData.playerEmotion;
        Emotion niaEmotion = currentRoomDialogueData.niaEmotion;

        if(isTyping == true)
        {
            //SkipLine(currentTalker,currnetText);
            return;
        }

        typingCoroutine = StartCoroutine(TypeText(speaker,speakerName,speakSpeed,text,playerEmotion,niaEmotion));

    }

    void SkipLine()
    {
        isTyping = true;




    }





    /////

    private Coroutine typingCoroutine;
    private IEnumerator TypeText( Speaker _speaker ,string _speakerName, SpeakSpeed _speakSpeed, string _text,
                                    Emotion _playerEmotion,Emotion _niaEmotion)
    {
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
            return 0.6f;
        else if(speakSpeed == SpeakSpeed.normal)
            return 0.3f;
        else if(speakSpeed == SpeakSpeed.fast)
            return 0.1f;

        return 0;
    }



}
