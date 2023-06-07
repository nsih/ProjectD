using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ChatBoxControl : MonoBehaviour
{
    private string chatDir = "/Resource/CSV/Chat";
    private string nameFile = "ChatNames.csv";
    private string chatsFile = "Chats.csv";

    public static List<string> nameParsedData;  //names parsed data
    public static List<ChatData> chatParsedData;  //chats parsed data

    List<ChatData> defaultChatData;


    GameObject chatView;
    GameObject[] chatBoxes;


    private bool isWaitChat = true;

    void Start()
    {
        chatView = GameObject.Find("ChatContent");

        chatBoxes = new GameObject[chatView.transform.childCount];

        for (int i = 0 ; i < 5 ; i++)
        {
            chatBoxes[i] = chatView.transform.GetChild(i).gameObject;

            chatBoxes[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            chatBoxes[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }

        NameParsing(nameFile);
        ChatsParsing(chatsFile);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(isWaitChat)
        {
            ChatUpdateCoroutine = StartCoroutine(ChatUpdating());
        }
    }


    void ChatBoxUpdate()
    {
        string name = nameParsedData[Random.Range(0, nameParsedData.Count-1)];

        //if(어딘가에서 받아온 플레그 데이터.)     현재 채팅 플레그 체크해줘 나중에..
        string chat = defaultChatData[Random.Range(0,defaultChatData.Count-1)].chat;

        for (int i = chatBoxes.Length - 1; i > 0; i--)
        {
            chatBoxes[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = chatBoxes[i-1].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
            chatBoxes[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = chatBoxes[i-1].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
        }

        chatView.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        chatView.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chat;
    }


    private Coroutine ChatUpdateCoroutine;
    private IEnumerator ChatUpdating()
    {
        isWaitChat = false;
        float uDelay = Random.Range(0.1f,1f);
        yield return new WaitForSeconds(uDelay);

        ChatBoxUpdate();
        isWaitChat = true;
    }


    
    #region "parsing"
    void NameParsing(string FileName)
    {
        nameParsedData = new List<string>();
        string filePath = Path.Combine(Application.dataPath + chatDir, FileName);
        StreamReader reader = new StreamReader(filePath);

        string firstLine = reader.ReadLine();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');

            nameParsedData.Add(data[1]);
        }
        reader.Close();
    }
    void ChatsParsing(string FileName)
    {
        chatParsedData = new List<ChatData>();
        string filePath = Path.Combine(Application.dataPath + chatDir, FileName);
        StreamReader reader = new StreamReader(filePath);

        string firstLine = reader.ReadLine();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');

            // Parsing  FLAG,INDEX,CONTEXT
            string flag = data[0];
            int index = int.Parse(data[1]);
            string chat = data[2];

            // 파싱된 데이터를 객체로 생성하여 리스트에 추가
            ChatData csvData = new ChatData(flag, index, chat);
            chatParsedData.Add(csvData);
        }
        reader.Close();

        //플레그에 따른 채팅데이터 분류
        defaultChatData = chatParsedData.FindAll(data => data.flag == "DEFAULT");
    }
    #endregion
}


public class ChatData
{
    public string flag;
    public int index;
    public string chat;

    public ChatData(string flag, int index, string chat)
    {
        this.flag = flag;
        this.index = index;
        this.chat = chat;
    }
}