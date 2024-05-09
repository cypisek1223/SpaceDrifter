using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NodeBehaviour;
using static Graph<NodeBehaviour>;

public class GraphPainter : GraphBehaviour
{
    public Material material;
    public float width = 1;

    private void Start()
    {
        myGraph.ForEachConnectionBFS(DrawConnection);
        myGraph.ForEachNode(SetMaterial);
    }

    void SetMaterial(Node node)
    {
        GraphPainterNode paintNode = node.Value as GraphPainterNode;
        if(paintNode.lineRender)
            paintNode.lineRender.material = material;
    }

    void DrawConnection(Node start, Node end, float weight)
    {
        var from = start.Value as GraphPainterNode;
        var to = end.Value as GraphPainterNode;
        from.Draw(to, weight * width);
    }
}
