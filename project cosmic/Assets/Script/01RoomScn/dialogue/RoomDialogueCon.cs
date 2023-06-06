using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class RoomDialogueCon : MonoBehaviour
{
    private string csvDir = "/Resource/CSV";
    private string roomScriptFile = "ScnRoomScript.csv";
    public static List<RoomScriptData> rsParsedData;   //room script parsed data

    float fastTypeSpeed = 0.03f;
    float normarTypeSpeed = 0.07f;
    float slowTypeSpeed = 0.3f;


    private int roomFlag = 0;
    private int currentIndex;

    GameObject playerTxt;
    GameObject niaTxt;

    private bool isTyping = false;



    private void Awake()
    {
        playerTxt = GameObject.Find("PlayerText");
        niaTxt = GameObject.Find("NiaText");


        Parser(roomScriptFile);
    }

    private void Start() 
    {
        StartDialogue();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ProceedNextLine();
        }
        
    }






    #region "parsing"
    void Parser(string FileName)    //csv파일로 init
    {
        rsParsedData = new List<RoomScriptData>();
        string filePath = Path.Combine(Application.dataPath + csvDir, FileName);
//        Debug.Log(filePath);

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
        /*
        foreach (RoomScriptData data in rsParsedData)
        {
            Debug.Log("Index: " + data.index +
                      ", Talker: " + data.talker +
                      ", Script: " + data.script +
                      ", Talk Speed: " + data.talkSpeed +
                      ", Stand Image: " + data.standImg +
                      "\n");
        }   
        */
    }
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
    #region "dialogue control"
    void StartDialogue()
    {
        currentIndex = 0;
        ShowDialogue(roomFlag);
    }

    void ShowDialogue(int currentFlag)
    {
        List<RoomScriptData> currentDialogueData = rsParsedData.FindAll(data => data.flag == currentFlag);
        
        
        if (currentIndex < currentDialogueData.Count)
        {
            RoomScriptData currentLine = currentDialogueData[currentIndex];

            string currentTalker = currentLine.talker;
            string currnetText = currentLine.script;
            float currentTalkSpeed = currentLine.talkSpeed;

            //skip
            if(isTyping == true)
            {
                SkipLine(currentTalker,currnetText);
                return;
            }

            
            
            // 대사 처리
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(currentTalker,currnetText,currentTalkSpeed));
        }
        else
        {
            EndDialogue();
        }
    }

    void ProceedNextLine()
    {
        ShowDialogue(roomFlag);
    }

    void SkipLine(string _currentTalker,string _currentDialogue)
    {
        Debug.Log("skip");
        StopCoroutine(typingCoroutine);

        if(_currentTalker == "player")
        {
            playerTxt.GetComponent<TextMeshProUGUI>().text = _currentDialogue;
        }

        else if(_currentTalker == "nia")
        {
            niaTxt.GetComponent<TextMeshProUGUI>().text = _currentDialogue;
        }

        
        isTyping = false;
        currentIndex++;
    }

    void EndDialogue()  //대기화면 시작
    {
        playerTxt.GetComponent<TextMeshProUGUI>().text = "";
        niaTxt.GetComponent<TextMeshProUGUI>().text = "";

        roomFlag++;
    }


    #region "coroutine define"
    private Coroutine typingCoroutine;
    private IEnumerator TypeText(string _currentTalker,string _currentDialogue,float _currentSpeed)
    {
        /*
        Debug.Log("_currentTalker : "+_currentTalker);
        Debug.Log("_currentDialogue : "+_currentDialogue);
        Debug.Log("_currentSpeed : "+_currentSpeed);
        */

        isTyping = true;
        if(_currentTalker == "player")
        {
            playerTxt.GetComponent<TextMeshProUGUI>().text = "";

            foreach (char c in _currentDialogue)
            {
                playerTxt.GetComponent<TextMeshProUGUI>().text += c;

                //Debug.Log(playerTxt.GetComponent<TextMeshProUGUI>().text);

                yield return new WaitForSeconds(_currentSpeed);
            }
            isTyping = false;
            currentIndex++;
            yield break;
        }

        else if(_currentTalker == "nia")
        {
            niaTxt.GetComponent<TextMeshProUGUI>().text = "";

            foreach (char c in _currentDialogue)
            {
                niaTxt.GetComponent<TextMeshProUGUI>().text += c;
                yield return new WaitForSeconds(_currentSpeed);
            }
            isTyping = false;
            currentIndex++;
            yield break;
        }
    }
    #endregion

    #endregion
}

public class RoomScriptData
{
    public int flag;
    public int index;
    public string talker;
    public string script;
    public float talkSpeed;
    public string standImg;

    public RoomScriptData(int flag, int index, string talker, string script, float talkSpeed, string standImg)
    {
        this.flag = flag;
        this.index = index;
        this.talker = talker;
        this.script = script;
        this.talkSpeed = talkSpeed;
        this.standImg = standImg;
    }
}
