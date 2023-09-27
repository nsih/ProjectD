using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;


public class mapGenerator : MonoBehaviour
{
    public static MapGraph<RoomType> mapGraph = new MapGraph<RoomType>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateMap(6,18);
        }
    }


    public void GenerateMap(int x,int y)
    {
        RemoveAllNodes(mapGraph);

        Room<RoomType> startRoom = new Room<RoomType>(true,RoomType.Start, 0, 0);
        mapGraph.AddNode(startRoom);


        System.Random random = new System.Random();

        List<int> FixedRowList = new List<int>();


        //고정층 정의
        int BattleRow0 = 1;
        FixedRowList.Add(1);

        int FixedEventRow0 = GenerateRandomRow( random, FixedRowList, 4, 9);
        int FixedEventRow1 = GenerateRandomRow( random, FixedRowList, 10, 15);
        int FixedEventRow2 = 18;
        FixedRowList.Add(18);

        int eliteBattleRow0 = GenerateRandomRow( random, FixedRowList, 4, 10);
        int eliteBattleRow1 = GenerateRandomRow( random, FixedRowList, 12, 18);


        
        for (int i = 1; i <= y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                RoomType roomType;

                if(i == BattleRow0)
                {
                    roomType = RoomType.Battle;
                }

                else if(i == eliteBattleRow0 || i == eliteBattleRow1)
                {
                    roomType = RoomType.EliteBattle;
                }

                else if(i == FixedEventRow0 || i == FixedEventRow1 || i == FixedEventRow2)
                {
                    roomType = RoomType.FixedEvent;
                }

                else
                {
                    roomType = GetRandomRoomType(random);
                }

                Room<RoomType> randomRoom = new Room<RoomType>(false, roomType, j, i);
                mapGraph.AddNode(randomRoom);
            }
        }

        AddMapPath();
    }

    int GenerateRandomRow (System.Random random, List<int> FixedRowList, int minValue, int maxValue)
    {
        int randomNumber;
        do
        {
            randomNumber = random.Next(minValue, maxValue + 1);
        } while (FixedRowList.Contains(randomNumber));

        FixedRowList.Add(randomNumber);
        return randomNumber;
    }
    
    //랜덤 룸타입 반환 (전투, 이벤트, 재단, 상점)
    public RoomType GetRandomRoomType(System.Random random)
    {
        // 각 RoomType에 대한 확률을 정의
        Dictionary<RoomType, int> roomTypeProbabilities = new Dictionary<RoomType, int>
        {
            { RoomType.Battle, 65 }, // 전투 확률 60%
            { RoomType.RandomEvent, 25 }, // 랜덤 이벤트 확률 30%
            { RoomType.Alter, 5 }, // 재단 확률 5%
            { RoomType.Shop, 5 }, // 재단 확률 5%
            { RoomType.NPC, 0 }, // NPC 확률 0%
        };

        int totalProbability = roomTypeProbabilities.Values.Sum();
        int randomValue = random.Next(0, totalProbability);

        foreach (var kvp in roomTypeProbabilities)
        {
            if (randomValue < kvp.Value)
                return kvp.Key;
            randomValue -= kvp.Value;
        }

        // 확률에 따라 RoomType이 선택되지 않은 경우, 기본값으로 시작을 반환합니다.
        Debug.Log("roomtype didnt choosed");
        return RoomType.Battle;
    }





    /// <summary>
    /// ////path
    /// </summary>

    //그래프 간선 넣고 리턴
    public void AddMapPath()
    {
        int maxX = mapGraph.Nodes.Max(node => node.X);
        int maxY = mapGraph.Nodes.Max(node => node.Y);


        Room<RoomType> startNode = mapGraph.Nodes.FirstOrDefault(node => node.X == 0 && node.Y == 0);
        //y = 0층과 1층의 6개 노드 모두 연결
        for(int i = 0; i <= maxX; i++)
        {
            Room<RoomType> x1node = mapGraph.Nodes.FirstOrDefault(node => node.X == i && node.Y == 1);

            if(startNode != null && x1node != null)
            {
                mapGraph.AddEdge(startNode, x1node);
            }
        }

        Room<RoomType> bossRoom = new Room<RoomType>(true,RoomType.Boss, 0, maxY+1);
        mapGraph.AddNode(bossRoom);

        //1층 전부를 dfs로 맨끝 (보스방) 까지 연결
        for(int i = 0 ; i <= maxX ; i++)
        {
            Room<RoomType> node = mapGraph.Nodes.FirstOrDefault(node => node.X == i && node.Y == 1);

            GoDFS(mapGraph,node,maxX,  mapGraph.Nodes.Max(node => node.Y)  );
        }

        RemoveIsolatedNodes(mapGraph);
    }

    static void GoDFS(MapGraph<RoomType> mapGraph, Room<RoomType> startNode, int maxX,int maxY)
    {
        System.Random random = new System.Random();
        int direction = random.Next(3);

        int toX = startNode.X;
        int toY = startNode.Y+1;

        if(toY == maxY)
        {
            toX = 0;
        }

        else
        {
            if (direction == 1 && toX != maxX && CanMoveRight(mapGraph, startNode))
            {
                toX++;
            }
            else if (direction == 2 && toX != 0 && CanMoveLeft(mapGraph, startNode))
            {
                toX--;
            }
        }


        Room<RoomType> toNode = mapGraph.Nodes.FirstOrDefault(node => node.X == toX && node.Y == toY);
        mapGraph.AddEdge(startNode, toNode);

        //Debug.Log("node x : " + toX + "\n    node y : " + toY);

        // return
        if (toY == maxY)
        {
            return;
        }

        // recursion
        GoDFS(mapGraph, toNode, maxX,maxY);
    }

    static bool CanMoveRight(MapGraph<RoomType> mapGraph, Room<RoomType> startNode)
    {
        return !mapGraph.Nodes.FirstOrDefault(node => node.X == startNode.X + 1 && node.Y == startNode.Y).Neighbors
            .Contains(mapGraph.Nodes.FirstOrDefault(node => node.X == startNode.X && node.Y == startNode.Y + 1));
    } 
    static bool CanMoveLeft(MapGraph<RoomType> mapGraph, Room<RoomType> startNode)
    {
        return !mapGraph.Nodes.FirstOrDefault(node => node.X == startNode.X - 1 && node.Y == startNode.Y).Neighbors
            .Contains(mapGraph.Nodes.FirstOrDefault(node => node.X == startNode.X && node.Y == startNode.Y + 1));
    }

    void RemoveAllNodes(MapGraph<RoomType> mapGraph)    //노드 클리어
    {
        foreach (var node in mapGraph.Nodes.ToList())
        {
            mapGraph.RemoveNode(node);
        }
    }

    void RemoveIsolatedNodes(MapGraph<RoomType> mapGraph)   //고립된 노드제거
    {
        List<Room<RoomType>> isolatedNodes = new List<Room<RoomType>>();

        foreach (var node in mapGraph.Nodes.ToList())
        {
            if (node.Neighbors.Count == 0 && node.Y != mapGraph.Nodes.Max(node => node.Y) )
            {
                isolatedNodes.Add(node);
                mapGraph.RemoveNode(node);
            }
        }
    }
}




//


public class Room<T>
{
    public bool Player; //player location
    public T RoomType { get; private set; }
    public List<Room<T>> Neighbors { get; private set; }
    public int X { get; set; } // x 좌표 정보
    public int Y { get; set; } // y 좌표 정보

    public Image roomPin;

    public Room(bool player, T roomType, int x, int y)
    {
        Player = player;

        RoomType = roomType;

        X = x;
        Y = y;

        Neighbors = new List<Room<T>>();

        roomPin = null;
    }

    public void AddPath(Room<T> neighbor)
    {
        Neighbors.Add(neighbor);
    }
}

public class MapGraph<T>
{
    public List<Room<T>> Nodes { get; private set; }

    public MapGraph()
    {
        Nodes = new List<Room<T>>();
    }

    public void AddNode(Room<T> node)
    {
        Nodes.Add(node);
    }

    public void AddEdge(Room<T> fromNode, Room<T> toRoom)
    {
        fromNode.AddPath(toRoom);
    }

    public void RemoveNode(Room<T> node)
    {
        Nodes.Remove(node);
        foreach (var otherNode in Nodes)
        {
            otherNode.Neighbors.Remove(node);
        }
    }
}


public enum RoomType : int
{
    Start = 0,
    Boss = 1,

    
    Battle = 2,
    EliteBattle = 3,

    FixedEvent = 4,
    RandomEvent = 5,
    
    Alter = 6,
    Shop = 7,
    NPC = 8
}