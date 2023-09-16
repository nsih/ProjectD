using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public void GenerateMap()
    {
        MapGraph<RoomType> mapGraph = new MapGraph<RoomType>();

        Room<RoomType> startRoom = new Room<RoomType>(RoomType.Start, 0, 0);
        mapGraph.AddNode(startRoom); // 시작 방을 그래프에 추가

        for (int i = 1; i < 16; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Room<RoomType> room = new Room<RoomType>(RoomType.Start, i, j);
                mapGraph.AddNode(room); // 생성된 방을 그래프에 추가
            }
        }

        Room<RoomType> bossRoom = new Room<RoomType>(RoomType.Boss, 16, 0);
        mapGraph.AddNode(bossRoom);
    }

    
}


public class Room<T>
{
    public int X { get; set; } // x 좌표 정보
    public int Y { get; set; } // y 좌표 정보
    public T Data { get; private set; }
    public List<Room<T>> Neighbors { get; private set; }

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
}

