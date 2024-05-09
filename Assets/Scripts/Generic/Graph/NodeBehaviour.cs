using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Graph<NodeBehaviour>;

public class NodeBehaviour : MonoBehaviour
{
    [SerializeField]protected float fill;
    [SerializeField]protected float cap = 1; //capacity

    Node node;
    public Node Node { get { return node ?? (node = new Node(this)); } }

    public Connection[] connections;

    protected virtual void Awake()
    {
        foreach(var c in connections)
        {
            Node.AddNeighbour(c.node.Node, c.weight);
        }
    }

    public void Fill(float amt)
    {
        fill += amt;
        fill = Mathf.Clamp(fill, 0, cap);
    }
    public void SetFill(float fill)
    {
        this.fill = fill;
        this.fill = Mathf.Clamp(this.fill, 0, cap);
    }

    [Serializable]
    public struct Connection
    {
        public float weight;
        public NodeBehaviour node;
    }
}
