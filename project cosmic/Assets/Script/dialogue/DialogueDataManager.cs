using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class DialogueLineOption
{
    public int id;
    public string text;
    public bool isLastLine;
    public int? nextLineId;
}

[System.Serializable]
public class DialogueLine
{
    public int id;
    public string talker;
    public string emotion;
    public string text;
    public string talkSpeed;
    public bool isLastLine;
    public int? nextLineId;
    public DialogueLineOption[]? option;
}

[System.Serializable]
public class DialogueData
{
    public string title;
    public DialogueLine[] lines;
}


public class DialogueDataManager : MonoBehaviour
{
    //Room
    private string roomScriptFile = "JSON/RoomDialogueData";

    //역직렬화 데이터 담을 DialogueData
    public static DialogueData[] roomDialogueData;

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
            roomDialogueData = JsonConvert.DeserializeObject<DialogueData[]>(jsonFile.text); //역직렬화
        }

    }


    #region "return dailogue data value"
    public float TalkSpeed(string _talkSpeed)
    {
        if (_talkSpeed == null)
            return 0.07f;

        else if (_talkSpeed == "slow")
            return 0.3f;

        else if (_talkSpeed == "normal")
            return 0.07f;

        else if (_talkSpeed == "fast")
            return 0.03f;

        else
            Debug.Log("exeption error : " + _talkSpeed);

        return 0;
    }

    public emotion TalkerEmotion(string _emotion)
    {
        if (_emotion == null)
            return emotion.Default;

        else if (_emotion == "Default")
            return emotion.Default;

        else if (_emotion == "Happy")
            return emotion.Happy;

        else if (_emotion == "Sad")
            return emotion.Sad;

        else if (_emotion == "Angry")
            return emotion.Angry;

        else if (_emotion == "Shy")
            return emotion.Shy;

        else
            return emotion.Default;
    }

    #endregion

}



public enum emotion : int
{
    Default = 0,
    Happy = 1,
    Sad = 2,
    Angry = 3,
    Shy = 4
}

