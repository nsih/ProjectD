using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomScriptParser : MonoBehaviour
{
    private string RoomScriptFile = "ScnRoomScript.csv";
    private List<RoomScriptData> parsedData;

    private void Awake()
    {
        Parser(RoomScriptFile);
    }

    void Parser(string FileName)
    {
        parsedData = new List<RoomScriptData>();
        string filePath = Path.Combine(Application.streamingAssetsPath, FileName);

        Debug.Log(filePath);

        StreamReader reader = new StreamReader(filePath);

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');

            // Parsing
            int index = int.Parse(data[0]);
            string talker = data[1];
            string script = data[2];
            float talkSpeed = float.Parse(data[3]);
            string standImg = data[4];

            // 파싱된 데이터를 객체로 생성하여 리스트에 추가
            RoomScriptData csvData = new RoomScriptData(index, name, script, talkSpeed, standImg);
            parsedData.Add(csvData);
        }

        reader.Close();

        // 파싱된 데이터 확인
        foreach (RoomScriptData data in parsedData)
        {
            Debug.Log("Index: " + data.index +
                      ", Talker: " + data.name +
                      ", Script: " + data.script +
                      ", Talk Speed: " + data.talkSpeed +
                      ", Stand Image: " + data.standImg +
                      "\n");
        }
    }
}
