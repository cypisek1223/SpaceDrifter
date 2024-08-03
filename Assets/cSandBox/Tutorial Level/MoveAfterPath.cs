using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAfterPath : MonoBehaviour
{
    public float speed;
    public Transform[] points;
    public int index = 1;
    public Transform target;

    void Start()
    {
        index = 1;
        target = points[index];
    }

    void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, target.position) < 0.5)
        {
            
            if (index >= points.Length - 1)
            {
                index = 0;
                target = points[index];
            }
            else
            {
                index++;
                target = points[index];
            }
                
        }
    }
}
