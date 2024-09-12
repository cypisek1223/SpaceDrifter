using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public Transform pointA;  
    public Transform pointB;  
    public float speed = 1.0f; 

    private Transform targetPosition;

    void Start()
    {
        targetPosition = pointB;
        transform.position = pointA.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {

            targetPosition = targetPosition == pointA ? pointB : pointA;
        }
    }
}