using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{    
    GameObject gameManager;

    public static Dictionary<int, RoomData> map = new Dictionary<int, RoomData>();




    void Start ()
    {
        gameManager = GameObject.Find("GameManager");
    }

    #region "Quest"

    public void CheckStageQuest()
    {
        if(GameManager.currentStage == 0)
        {
            if(map.Count == CheckisRevealed())
            {
                GameManager.isQuestDone = true;
                Debug.Log("asd");
            }
        }

        else if(GameManager.currentStage == 1)
        {
            CheckMental();
        }


        else
        {
            Debug.Log("stage : "+ GameManager.currentStage );
        }
    }

    public int CheckisRevealed()  //모든 방을 탐험
    {
        int count = 0;
        foreach (var kvp in map)
        {
            RoomData node = kvp.Value;
            int key = kvp.Key;

            if (map[key].isRevealed == true)
            {
                count++;
            }
        }

        return count;
    }

    void CheckMental()
    {
        if(GameManager.mentality == 0)
        {
            GameManager.isQuestDone = true;
        }
    }

    #endregion



    #region "map Generate"
    public void GenerateNewStage()    //기본 외형은 정해져 있음. Search는 false로 함
    {
        map.Clear();

        if(GameManager.currentStage == 0 )
        {
            GenateStage0();
        }

        else if(GameManager.currentStage == 1)
        {
            GenateStage1();
        }
    }

    void GenateStage0()
    {
        //add nodes to dictionary(roomNode)
        for(int i = 0 ; i < 8 ; i++)
        {
            map.Add(i, new RoomData(i, RoomType.Null, false, false,false));
        }

        //initialize dictionary's node value
        foreach (var kvp in map)   //key-value pair
        {
            RoomData node = kvp.Value;
            int key = kvp.Key;

            //  initialize

            if(key == 0)
            {
                node.roomType = RoomType.Null;      //룸 타입 결정

                node.isRevealed = true; //0은 시작노드

                node.AddConnectedNode(map[1]);  //연결
            }

            else if(key == 1)
            {
                node.roomType = RoomType.Altar;

                node.AddConnectedNode(map[0]);
                node.AddConnectedNode(map[2]);
            }
            
            else if(key == 2)
            {
                node.roomType = RoomType.Battle;

                //여기만 텔이 이썽요
                node.isTp = true;

                node.AddConnectedNode(map[1]);
                node.AddConnectedNode(map[3]);
                node.AddConnectedNode(map[6]);
            }

            else if(key == 3)
            {
                node.roomType = RoomType.Test;

                node.AddConnectedNode(map[2]);
                node.AddConnectedNode(map[4]);
            }

            else if(key == 4)
            {
                node.roomType = RoomType.Shop;

                node.AddConnectedNode(map[3]);
                node.AddConnectedNode(map[5]);
            }

            else if(key == 5)
            {
                node.roomType = RoomType.Altar;

                node.AddConnectedNode(map[4]);
            }

            else if(key == 6)
            {
                node.roomType = RoomType.Test;

                node.AddConnectedNode(map[2]);
                node.AddConnectedNode(map[7]);
            }

            else if(key == 7)
            {
                node.roomType = RoomType.Event;

                node.AddConnectedNode(map[6]);
            }
        }
    }
    void GenateStage1()
    {

    }
    
    #endregion


    //인접 맵 찾기
    public List<int> FindAttachedKey(int key)
    {
        // key에 해당하는 node
        RoomData targetNode = null;
        if (map.ContainsKey(key))
        {
            targetNode = map[key];
        }

        // targetNode와 연결된 노드의 key
        List<int> connectedNodeKeys = new List<int>();
        if (targetNode != null)
        {
            foreach (RoomData connectedNode in targetNode.GetConnectedNodes())
            {
                connectedNodeKeys.Add(connectedNode.roomNum);
            }
        }
        return connectedNodeKeys;
    }
    
    public void AddTpConnect()  ////is TP는 is clear 보고 연결. 
    {

        if(map[GameManager.currentRoom].isTp && map[GameManager.currentRoom].isClear) 
        {
            foreach (var kvp in map)   //key-value pair
            {
                RoomData node = kvp.Value;
                int key = kvp.Key;

                //이미 연결 안돼있는 방에 연결 추가 (체크는 미니맵 열면 해줌)
                if(key != GameManager.currentRoom && !FindAttachedKey(key).Contains(GameManager.currentRoom))
                {
                    node.AddConnectedNode(map[GameManager.currentRoom]); 
                }
            }
        }
    }
    //방 움직이면 방 이벤트 시작(시작안하면 안하는)
    public void StartRoomEventPhase(RoomType roomType)
    {
        //땅 로드 없음.
        //일단 다 actionphase로 고정해뒀음

        if(roomType == RoomType.Null)
        {
            GameManager.isActionPhase = false;

            EndRoomEventPhase(roomType);

            Debug.Log("Start"+roomType);
        }

        else if(roomType == RoomType.Battle)
        {
            GameManager.isActionPhase = false;

            this.gameObject.GetComponent<BattleEventManager>().GenBattleRoom(GameManager.currentStage);


            Debug.Log("Start"+roomType);
        }

        else if(roomType == RoomType.Test)
        {
            GameManager.isActionPhase = false;
            
            EndRoomEventPhase(roomType);


            Debug.Log("Start"+roomType);
        }

        else if(roomType == RoomType.Altar)
        {
            GameManager.isActionPhase = false;

            EndRoomEventPhase(roomType);


            Debug.Log("Start"+roomType);
        }

        else if(roomType == RoomType.Shop)
        {
            GameManager.isActionPhase = false;

            EndRoomEventPhase(roomType);


            Debug.Log("Start"+roomType);
        }

        else if(roomType == RoomType.Event)
        {
            GameManager.isActionPhase = false;

            EndRoomEventPhase(roomType);
            

            Debug.Log("Start"+roomType);
        }

        else if(roomType == RoomType.Boss)
        {
            GameManager.isActionPhase = false;

            EndRoomEventPhase(roomType);
            

            Debug.Log(roomType);
        }
    }


    public void EndRoomEventPhase(RoomType roomType)    //보상도 주고 마 다했어.
    {
        if(roomType == RoomType.Null)
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;

            Debug.Log("End"+roomType);
        }

        else if(roomType == RoomType.Battle)
        {
            GameManager.isActionPhase = true;

            gameObject.GetComponent<ArtifactManager>().OpenArtifactRewardPopup();
            gameObject.GetComponent<ArtifactManager>().SuggestArtifacts();  


            Debug.Log("End"+roomType);
        }

        else if(roomType == RoomType.Test)
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;
            
            Debug.Log("End"+roomType);
        }

        else if(roomType == RoomType.Altar)
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;
            
            Debug.Log("End"+roomType);
        }

        else if(roomType == RoomType.Shop)
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;
            
            Debug.Log("End"+roomType);
        }

        else if(roomType == RoomType.Event)
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;
            
            Debug.Log("End"+roomType);
        }

        else if(roomType == RoomType.Boss)
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;
            
            Debug.Log("End"+roomType);
        }
        

        else
        {
            map[GameManager.currentRoom].isClear = true;
            GameManager.isActionPhase = true;
            
            Debug.Log("End Error : "+roomType);
        }
    }

}


public class RoomData
{
    public int roomNum;     //graph Number
    public RoomType roomType;   //type
    public bool isRevealed;   //revealed
    public bool isTp;  //is Tp exist?
    public bool isClear;     //clear

    private List<RoomData> connectedNodes;  //connected nodes

    public RoomData(int roomNum, RoomType type, bool isRevealed, bool isTp,bool isClear)
    {
        this.roomNum = roomNum;
        this.roomType = type;
        this.isRevealed = isRevealed;
        this.isTp = isTp;
        this.isClear = isClear;

        connectedNodes = new List<RoomData>();
    }

    public void AddConnectedNode(RoomData node)
    {
        connectedNodes.Add(node);
    }

    public List<RoomData> GetConnectedNodes()
    {
        return connectedNodes;
    }
}
public enum RoomType : int
{
    Null = 0,
    Altar = 1,
    Battle = 2,
    Shop = 3,
    Test = 4,
    Event = 5,
    Boss = 6

}