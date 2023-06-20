using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    //title



    //Room
    private string csvDir = "/Resource/CSV";
    private string roomScriptFile = "ScnRoomScript.csv";
    public static List<RoomScriptData> rsParsedData;   //room script parsed data
    private static int roomFlag;

    
    //Land



    void RoomScriptDataParser(string FileName)
    {

    }

    void LandScriptDataParser(string FileName)
    {

    }

}




public class RoomScriptDatas
{
    public int flag;
    public int index;
    public string talker;
    public string script;
    public float talkSpeed;
    public string standImg;

    public RoomScriptDatas(int flag, int index, string talker, string script, float talkSpeed, string standImg)
    {
        this.flag = flag;
        this.index = index;
        this.talker = talker;
        this.script = script;
        this.talkSpeed = talkSpeed;
        this.standImg = standImg;
    }
}



public class LandScriptData
{
    public int flag;
    public int index;
    public string talker;
    public string script;
    public float talkSpeed;
    public string standImg;

    public LandScriptData(int flag, int index, string talker, string script, float talkSpeed, string standImg)
    {
        this.flag = flag;
        this.index = index;
        this.talker = talker;
        this.script = script;
        this.talkSpeed = talkSpeed;
        this.standImg = standImg;
    }
}
