using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{    
    private Dictionary<int, RoomData> map;



    #region "map Generate"
    void GenerateRoom()    //기본 외형은 정해져 있음. Search는 false로 함
    {
        if(GameManager.currentStage == 0 )
        {
            GenateStage0();
        }

        else if(GameManager.currentStage == 1)
        {
            GenateStage1();
        }
    }

    #region "map Generator, "
    void GenateStage0()
    {
        //add nodes to dictionary(roomNode)
        for(int i = 0 ; i < 7 ; i++)
        {
            map.Add(i, new RoomData(i, RoomType.Null, false, false));
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

                node.AddConnectedNode(map[1]);
                node.AddConnectedNode(map[3]);
                node.AddConnectedNode(map[6]);
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

    #endregion

    //옆 맵 찾기
    public List<RoomData> FindAttachedNode(int key)
    {
        // find node (use key)
        RoomData targetNode = null;
        if (map.ContainsKey(key))
        {
            targetNode = map[key];
        }

        List<RoomData> connectedNodes = new List<RoomData>();
        if (targetNode != null)
        {
            foreach (RoomData connectedNode in targetNode.GetConnectedNodes())
            {
                connectedNodes.Add(connectedNode);
            }
        }

        return connectedNodes;
    }

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
    
}

public class RoomData
{
    public int roomNum;     //graph Number
    public RoomType roomType;   //type
    public bool isRevealed;   //revealed
    public bool isDone;     //clear

    private List<RoomData> connectedNodes;  //connected nodes

    public RoomData(int roomNum, RoomType type, bool isRevealed, bool isDone)
    {
        this.roomNum = roomNum;
        this.roomType = type;
        this.isRevealed = isRevealed;
        this.isDone = isDone;

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