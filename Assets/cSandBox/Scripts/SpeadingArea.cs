using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeadingArea : MonoBehaviour
{

    private Vector2 originalVelocity;
    private Rigidbody2D playerRb;
    private Coroutine restoreSpeedCoroutine;

    [SerializeField] float speedMultiplier = 1.2f;
    [SerializeField] float boostDuration = 2f;
    [SerializeField] float decetionRadius = 0.5f;
    [SerializeField] LayerMask boostSpeed;

    private void OnAreBoost()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, decetionRadius, boostSpeed);

        if (collider != null)
        {
            if (restoreSpeedCoroutine != null)
            {
                StopCoroutine(restoreSpeedCoroutine);
                restoreSpeedCoroutine = null;
            }

            originalVelocity = playerRb.velocity;

            playerRb.velocity *= speedMultiplier;
        }
        else
        {
            restoreSpeedCoroutine = StartCoroutine(nameof(RestoreSpeedAfterDelay));
        }


    }

    private IEnumerable RestoreSpeedAfterDelay()
    {
        Debug.Log("END speeding");
        yield return new WaitForSeconds(boostDuration);

        playerRb.velocity = originalVelocity;

        restoreSpeedCoroutine = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("START speeding");
            playerRb = collision.GetComponent<Rigidbody2D>();

            if(playerRb != null)
            {
                
                if(restoreSpeedCoroutine != null)
                {
                    StopCoroutine(restoreSpeedCoroutine);
                    restoreSpeedCoroutine = null;
                }

                originalVelocity = playerRb.velocity;

                playerRb.velocity *= speedMultiplier;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("EXIT");
            restoreSpeedCoroutine = StartCoroutine(nameof(RestoreSpeedAfterDelay));
        }
    }

  

}
