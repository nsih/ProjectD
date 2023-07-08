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
            Genator0();
        }

        else if(GameManager.currentStage == 1)
        {
            Genator1();
        }
    }

    #region "map 0~0 Generate"
    void Genator0()
    {
        //add nodes to dictionary(roomNode)
        for(int i = 0 ; i < 5 ; i++)
        {
            map.Add(i, new RoomData(i, RoomType.Null, false, false));
        }


        //initialize dictionary's node value
        foreach (var kvp in map)   //key-value pair
        {
            RoomData node = kvp.Value;
            int key = kvp.Key;
            // initialize

            if(key == 0)
            {
                node.type = RoomType.Null;
            }
        }

        //map.clear();

    }
    void Genator1()
    {

    }
    #endregion


    #endregion


    public List<RoomData> FindNodeByKey(int key)
    {
        // 주어진 key에 해당하는 노드를 찾습니다.
        RoomData targetNode = null;
        if (map.ContainsKey(key))
        {
            targetNode = map[key];
        }

        // targetNode와 연결된 엣지로 연결된 노드들을 찾습니다.
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

}

public class RoomData
{
    public int roomNum;     //graph Number
    public RoomType type;   //type
    public bool isRevealed;   //revealed
    public bool isDone;     //clear

    private List<RoomData> connectedNodes;  //connected nodes

    public RoomData(int roomNum, RoomType type, bool isRevealed, bool isDone)
    {
        this.roomNum = roomNum;
        this.type = type;
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
    Boss = 5
}