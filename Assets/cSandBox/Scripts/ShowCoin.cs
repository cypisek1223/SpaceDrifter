using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCoin : MonoBehaviour
{
    [SerializeField] GameObject[] coinsToShow;
    [SerializeField] float duration = 5f;
    [SerializeField] float couldown = 0.5f;

    private void Awake()
    {
        Debug.Log(transform.position);
        StartCoroutine(nameof(ShowCoins));
    }
    IEnumerator ShowCoins()
    {
        foreach (GameObject coin in coinsToShow)
        {
            coin.SetActive(true);
            Debug.Log("Spawn Coin");
            yield return new WaitForSeconds(couldown);

        }
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
