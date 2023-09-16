using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;


public class MapGenerator : MonoBehaviour
{
    static MapGraph<RoomType> mapGraph = new MapGraph<RoomType>();


    public void GenerateMap(int x,int y)
    {
        Room<RoomType> startRoom = new Room<RoomType>(RoomType.Start, 0, 0);
        mapGraph.AddNode(startRoom);

        // System.Random의 인스턴스 생성
        System.Random random = new System.Random();

        for (int i = 1; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                RoomType roomType;

                if(i == System.Math.Round(  (double)(y/3)*1   ) 
                || i == System.Math.Round(  (double)(y/3)*2   )
                || i == y-2   )
                    roomType = RoomType.FixedEvent;
                else
                    roomType = GetRandomRoomType(random);

                Room<RoomType> room = new Room<RoomType>(roomType, i, j);
                mapGraph.AddNode(room);


                if(i == y-1)
                    return;
            }
        }
    }

    public RoomType GetRandomRoomType(System.Random random)
    {
        // 각 RoomType에 대한 확률을 정의
        Dictionary<RoomType, int> roomTypeProbabilities = new Dictionary<RoomType, int>
        {
            { RoomType.Battle, 70 }, // 전투 확률 70%
            { RoomType.RandomEvent, 25 }, // 랜덤 이벤트 확률 25%
            { RoomType.Alter, 5 }, // 재단 확률 5%
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

    //그래프 받아서 간선 넣고 리턴
    public MapGraph<RoomType> AddMapPath(MapGraph<RoomType> mapGraph)
    {
        int maxX = mapGraph.Nodes.Max(node => node.X);
        int maxY = mapGraph.Nodes.Max(node => node.Y);


        Room<RoomType> startNode = mapGraph.Nodes.FirstOrDefault(node => node.X == 0 && node.Y == 0);

        for(int i = 0; i <= maxX; i++)
        {
            Room<RoomType> x1node = mapGraph.Nodes.FirstOrDefault(node => node.X == 1 && node.Y == i);

            if(startNode != null && x1node != null)
            {
                mapGraph.AddEdge(startNode, x1node);
            }
        }


        for(int i = 1; i <=maxY ; i++)
        {
            for(int j = 0 ; j <= maxX ; j++)
            {
                Room<RoomType> node = mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i);

                GoDFS(mapGraph,node,maxX,maxY);
            }
        }

        return mapGraph;
    }

    static void GoDFS(MapGraph<RoomType> mapGraph, Room<RoomType> startNode, int maxX,int maxY)
    {
        System.Random random = new System.Random();
        int direction = random.Next(3);

        int toX = startNode.X;
        int toY = startNode.Y+1;

        if (direction == 1)
        {
            if(toX + 1 <= maxX)
            {
                toX++;
            }
        }
        else if (direction == 2)
        {
            if(toX - 1 >= 0)
            {
                toX--;
            }
        }

        Room<RoomType> toNode = mapGraph.Nodes.FirstOrDefault(node => node.X == toX && node.Y == toY);
        mapGraph.AddEdge(startNode, toNode);

        // return
        if (toY == maxY)
        {
            return;
        }

        // 재귀호출
        GoDFS(mapGraph, toNode, maxX,maxY);
    }

    void RemoveAllNodes(MapGraph<RoomType> mapGraph)
    {
        foreach (var node in mapGraph.Nodes.ToList())
        {
            mapGraph.RemoveNode(node);
        }
    }

}




//


public class Room<T>
{
    public bool pl; //player location
    public T Data { get; private set; }
    public List<Room<T>> Neighbors { get; private set; }
    public int X { get; set; } // x 좌표 정보
    public int Y { get; set; } // y 좌표 정보

    public Room(T data, int x, int y)
    {
        Data = data;

        X = x;
        Y = y;

        Neighbors = new List<Room<T>>();
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