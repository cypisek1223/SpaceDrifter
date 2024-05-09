using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T> //directional weighted one
{
    public Node Root { get; private set; }
    public int NodeCount => nodes.Count;
    List<Node> nodes;

    public Graph()
    {
        nodes = new List<Node>();
    }

    public void ForEachNode(Action<Node> process)
    {
        foreach( var n in nodes ) 
        {
            process(n);
        }
    }

    internal void ForEachConnectionBFS(Action<Node, Node, float> process)
    {
        foreach(var node in nodes)
        {
            foreach(var neighbour_and_weight in node.neighbours)
            {
                process(node, neighbour_and_weight.Key, neighbour_and_weight.Value);
            }
        }
    }

    public bool CreateNode(Node node)
    {
        if(nodes.Contains(node))
        {
            return false; // Node already on the list
        }
        nodes.Add(node);
        if (nodes.Count == 1)
            Root = node;
        return true;
    }

    public class Node
    {
        public T Value { get; private set; }
        //float weight;
        public Dictionary<Node, float> neighbours { get; private set; }

        public Node(T v)
        {
            Value = v;
            neighbours = new Dictionary<Node, float>();
        }

        public bool AddNeighbour(Node neighbour, float weight)
        {
            if(neighbours.ContainsKey(neighbour))
            {
                //already a neighbour
                return false;
            }
            neighbours.Add(neighbour, weight);
            return true;
        }
    }
}
