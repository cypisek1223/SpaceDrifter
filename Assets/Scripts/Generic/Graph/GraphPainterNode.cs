using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPainterNode : NodeBehaviour
{
    public LineRenderer lineRender { get; private set;}
    public Color color;
    public SpriteRenderer sr;

    protected void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        sr.color = color;
    }

    private void Update()
    {
        float scale_y = (fill / cap);
        sr.transform.localPosition = -Vector3.up * (1 - scale_y) * 0.5f;
        sr.transform.localScale = Vector3.up * scale_y + new Vector3(1,0,1);
    }

    private void OnValidate()
    {
        if (!sr)
            sr = GetComponent<SpriteRenderer>();
        sr.color = this.color;
    }

    private void OnMouseDown()
    {
        Fill(0.1f);
        FillNeighbours();
    }

    protected void FillNeighbours()
    {
        foreach( var n in Node.neighbours )
        {
            GraphPainterNode node = n.Key.Value as GraphPainterNode;
            node.SetFill(fill);
            node.FillNeighbours();
        }
    }

    public void Draw(GraphPainterNode to, float width)
    {
        if(!lineRender)
        {
            lineRender = gameObject.AddComponent<LineRenderer>();
            lineRender.positionCount = 0;
        }

        lineRender.positionCount += 2;
        lineRender.SetPosition(lineRender.positionCount-2, transform.position);
        lineRender.SetPosition(lineRender.positionCount - 1, to.transform.position);
        lineRender.startWidth = width;
        lineRender.endWidth = width;
    }
}
