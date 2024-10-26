using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoin : MonoBehaviour
{

    [SerializeField] GameObject otherCoins;

    private void OnDestroy()
    {
        Instantiate(otherCoins, transform.position, Quaternion.identity);
    }
}
