using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueData : MonoBehaviour
{
    //title



    //Room
    private string csvDir = "/Resource/CSV";
    private string roomScriptFile = "ScnRoomScript.csv";
    public static List<RoomScriptData> rsParsedData;   //room script parsed data
    private static int roomFlag;

    
    //Land


    //common
    float fastTypeSpeed = 0.03f;
    float normarTypeSpeed = 0.07f;
    float slowTypeSpeed = 0.3f;

    void Awake()
    {
        RoomScriptDataParser(roomScriptFile);
    }





    #region "Pasing func"

    void RoomScriptDataParser(string FileName)
    {
        rsParsedData = new List<RoomScriptData>();
        string filePath = Path.Combine(Application.dataPath + csvDir, FileName);

        StreamReader reader = new StreamReader(filePath);

        string firstLine = reader.ReadLine();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');

            // Parsing
            int flag = int.Parse(data[0]);
            int index = int.Parse(data[1]);
            string talker = data[2];
            string script = data[3];
            float talkSpeed = talkSpeedCheck(data[4]);
            string standImg = data[5];

            // 파싱된 데이터를 객체로 생성하여 리스트에 추가
            RoomScriptData csvData = new RoomScriptData(flag, index, talker, script, talkSpeed, standImg);
            rsParsedData.Add(csvData);

        }
        reader.Close();
    }



    //common
    float talkSpeedCheck(string _talkSpeedS)
    {
        float tempSpeed = 0;

        //delay time
        if(_talkSpeedS == "FAST")
            tempSpeed = fastTypeSpeed;
        else if(_talkSpeedS == "NORMAL")
            tempSpeed = normarTypeSpeed;
        else if(_talkSpeedS == "SLOW")
            tempSpeed = slowTypeSpeed;

        return tempSpeed;
    }
    
    #endregion

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
