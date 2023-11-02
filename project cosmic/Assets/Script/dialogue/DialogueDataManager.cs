using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class RoomDialogueNode
{
    public int id;
    public string talker;
    public string emotion;
    public string text;
    public string talkSpeed;
    public string nextLineId;
    public bool isEnd;

    public DialogueOption[] Options;

    [System.Serializable]
    public class DialogueOption
    {
        public int id;
        public string text;
        public int nextLineId;
        public bool isEnd;
    }
}

[System.Serializable]
public class RoomDialogueData
{
    public Dictionary<string,List<RoomDialogueNode>> node;
}



public class DialogueDataManager : MonoBehaviour
{
    //Room
    //private string csvDir = "/Resource/Json";
    private string roomScriptFile = "JSON/RoomDialogueData";

    public static RoomDialogueData roomDialogueData;


    //common
    float fastTypeSpeed = 0.03f;
    float normarTypeSpeed = 0.07f;
    float slowTypeSpeed = 0.3f;

    void Awake()
    {
        RoomScriptDataParser(roomScriptFile);
    }

    void RoomScriptDataParser(string fileName)
    {
        //파일 있으면 파싱해서 데이터 넣기
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile != null)
        {
            string jsonString = jsonFile.text;
            roomDialogueData     = JsonUtility.FromJson<RoomDialogueData>(jsonString);  //파싱 - 역직렬화
        }
        
        //역직렬화 한거 사용하는 예
        foreach (KeyValuePair<string, List<RoomDialogueNode>> entry in roomDialogueData.node)
        {
            string nodeId = entry.Key;
            List<RoomDialogueNode> dialogueNodes = entry.Value;

            foreach (RoomDialogueNode node in dialogueNodes)
            {
                int id = node.id;
                string talker = node.talker;
                string emotion = node.emotion;
                string text = node.text;
                string talkSpeed = node.talkSpeed;
                string nextLineId = node.nextLineId;
                bool isEnd = node.isEnd;

                if (node.Options != null)
                {
                    foreach (RoomDialogueNode.DialogueOption option in node.Options)
                    {
                        int optionId = option.id;
                        string optionText = option.text;
                        int optionNextLineId = option.nextLineId;
                        bool optionIsEnd = option.isEnd;
                    }
                }
            }
        }
    }

    
    #region "return dailogue data value"
    public float TalkSpeed(string _talkSpeed)
    {
        if(_talkSpeed == null)
            return 0.07f;

        else if(_talkSpeed == "slow")
            return 0.3f;

        else if(_talkSpeed == "normal")
            return 0.07f;

        else if(_talkSpeed == "fast")
            return 0.03f;

        else
            Debug.Log("exeption error : "+_talkSpeed);
        
        return 0;
    } 

    public emotion TalkerEmotion(string _emotion)
    {
        if(_emotion == null)
            return emotion.Default;

        else if(_emotion == "Default")
            return emotion.Default;
        
        else if(_emotion == "Happy")
            return emotion.Happy;

        else if(_emotion == "Happy")
            return emotion.Happy;

        else if(_emotion == "Happy")
            return emotion.Happy;

        else if(_emotion == "Happy")
            return emotion.Happy;

        else
            return emotion.Default;
    }

    #endregion



}



public enum emotion:int
{
    Default = 0,
    Happy = 1,
    Sad = 2,
    Angry = 3,
    Shy = 4
}

