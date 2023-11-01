using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewRoomDialogueData", menuName = "RoomDialogueData")]
public class RoomDialogueData : ScriptableObject
{
    public string dialogueTitle;



    [System.Serializable]
    public struct DialogueLine
    {
        public LineType lineType;
        public Speaker speaker;
        public string speakerName;
        
        //보여줄 Img
        public Emotion talkerEmotion;

        public Volume volume;
        public SpeakSpeed speakSpeed;


        [TextArea(2, 22)]
        public string text;
    }

    public DialogueLine[] dialogues;
}

public enum LineType
{
    Line,
    Option
}

public enum Speaker
{
    Player,
    Nia
}

public enum Emotion
{
    Default,
    Happy,
    Sad,
    Angry,
    Shy
}

public enum SpeakSpeed
{
    slow,
    normal,
    fast
}

public enum Volume
{
    small,
    normal,
    loud
}