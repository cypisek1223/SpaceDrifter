using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Graph<NodeBehaviour>;

public class GraphBehaviour : MonoBehaviour
{
    public NodeBehaviour[] Nodes => nodes;
    [SerializeField] NodeBehaviour[] nodes;
    protected Graph<NodeBehaviour> myGraph;

    private void Awake()
    {
        myGraph = new Graph<NodeBehaviour>();
        foreach(NodeBehaviour n in nodes)
        {
            myGraph.CreateNode(n.Node);
        }
    }
}
