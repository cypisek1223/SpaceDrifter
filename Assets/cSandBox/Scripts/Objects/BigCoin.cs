using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoin : MonoBehaviour
{

    [SerializeField] GameObject otherCoins;
    [SerializeField] Vector3 coinsPosition;
    private void OnDestroy()
    {
        GameObject coins = Instantiate(otherCoins, transform.position + coinsPosition, Quaternion.identity);
    }
}
