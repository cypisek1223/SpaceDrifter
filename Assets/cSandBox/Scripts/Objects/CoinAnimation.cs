using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] GameObject destinationPoint;
    [SerializeField] GameObject gameObjectToSpawn;
    [SerializeField] float speed = 5;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinationPoint.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destinationPoint.transform.position)<= 0.5f)
        {
            Destroy(this.gameObject);
        }
    }


}
