using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewRoomDialogueData", menuName = "RoomDialogueData")]
public class RoomDialogueData : ScriptableObject
{
    public int dialogueID;
    public string dialogueName;



    [System.Serializable]
    public struct DialogueLine
    {
        public Speaker speaker;
        public string speakerName;
        
        
        //보여줄 Img
        public Emotion playerEmotion;
        public Emotion niaEmotion;

        public Volume volume;
        public SpeakSpeed speakSpeed;


        [TextArea(2, 22)]
        public string text;
    }

    public DialogueLine[] dialogues;
}

public enum Speaker
{
    Player,
    Nia,

}

public enum Emotion
{
    normal,
    smile,
    sad,
    Anger,
    Panic,
    curious
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