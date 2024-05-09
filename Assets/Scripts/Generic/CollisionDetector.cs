using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private string tagToDetect; // ignore tags if empty

    [SerializeField] bool singleUse;
    [SerializeField] private UnityEvent<Transform> OnDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayer = 1 << collision.gameObject.layer; // eg. layer 3 -> 0(x24) 0000 1000
        bool matchLayer = (otherLayer & detectionMask) != 0;
        if(matchLayer)
        {
            if(collision.CompareTag(tagToDetect) || string.IsNullOrEmpty(tagToDetect))
            {
                OnDetected.Invoke(collision.transform);
                if (singleUse)
                    Destroy(gameObject);
            }
        }
    }
}
