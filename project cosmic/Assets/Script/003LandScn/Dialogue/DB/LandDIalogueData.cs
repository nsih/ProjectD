using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLandDialogueData", menuName = "LandDialogueData")]
public class LandDialogueData : ScriptableObject
{
    public int dialogueID;
    public string dialogueName;



    [System.Serializable]
    public struct DialogueLine
    {
        public LandSpeaker speaker;
        public string speakerName;
        
        
        //보여줄 Img
        public Emotion playerEmotion;
        public Emotion opponentEmotion;

        public Volume volume;
        public SpeakSpeed speakSpeed;


        [TextArea(2, 22)]
        public string text;
    }

    public DialogueLine[] dialogues;
}

public enum LandSpeaker
{
    Player,
    Nia,
    Astoria
}