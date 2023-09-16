using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}


public class Node<T>
{
    public T Data { get; private set; }
    public List<Node<T>> Neighbors { get; private set; }

    public Node(T data)
    {
        Data = data;
        Neighbors = new List<Node<T>>();
    }

    public void AddPath(Node<T> neighbor)
    {
        Neighbors.Add(neighbor);
    }
}

public class DirectedGraph<T>
{
    public List<Node<T>> Nodes { get; private set; }

    public DirectedGraph()
    {
        Nodes = new List<Node<T>>();
    }

    public void AddNode(Node<T> node)
    {
        Nodes.Add(node);
    }

    public void AddEdge(Node<T> fromNode, Node<T> toNode)
    {
        fromNode.AddPath(toNode);
    }
}

