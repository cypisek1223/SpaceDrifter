using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SpeedingArea : MonoBehaviour
    {
        private Vector2 originalVelocity;
        private Coroutine restoreSpeedCoroutine;
        private bool boosting = false;

        [SerializeField] Rigidbody2D playerRb;
        [SerializeField] PlayerController playerController;

        [SerializeField] float speedMultiplier = 1.2f;
        [SerializeField] float boostDuration = 2f;
        [SerializeField] float detectionRadius = 0.5f;
        [SerializeField] LayerMask boostSpeed;

        private void Update()
        {
            OnAreaBoost();
        }

        private void OnAreaBoost()
        {
            // Sprawdza, czy gracz jest w obszarze przyspieszenia
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, boostSpeed);

            if (colliders.Length > 0)
            {
                // Jeœli gracz jest w obszarze przyspieszenia
                boosting = true;

                if (restoreSpeedCoroutine != null)
                {
                    StopCoroutine(restoreSpeedCoroutine);
                    restoreSpeedCoroutine = null;
                }

                playerController.smokeParticles.SetActive(false);
                playerController.fireParticles.SetActive(false);
                playerController.speedingParticles.SetActive(true);

                originalVelocity = playerRb.velocity;

                // Oblicz kierunek na podstawie obrotu statku
                Vector2 direction = playerRb.transform.up; // "up" wskazuje kierunek, w którym patrzy statek

                // Zwiêksz prêdkoœæ tylko w kierunku obrotu
                playerRb.velocity = direction * (originalVelocity.magnitude * speedMultiplier);

                Debug.Log("BOOST");
                //Handheld.Vibrate();
            }
            else
            {
                // Jeœli gracz opuœci³ obszar przyspieszenia i nadal by³ boostowany
                if (boosting && restoreSpeedCoroutine == null)
                {
                    restoreSpeedCoroutine = StartCoroutine(RestoreSpeedAfterDelay());
                }
            }
        }

        private IEnumerator RestoreSpeedAfterDelay()
        {
            Debug.Log("END speeding");
            boosting = false;

            // Czekaj okreœlony czas przed przywróceniem prêdkoœci
            yield return new WaitForSeconds(boostDuration);

            // Wy³¹cz cz¹steczki przyspieszenia, w³¹cz inne efekty
            playerController.smokeParticles.SetActive(true);
            playerController.fireParticles.SetActive(true);
            playerController.speedingParticles.SetActive(false);  // Wy³¹cz efekty przyspieszenia

            // Przywróæ oryginaln¹ prêdkoœæ gracza
            playerRb.velocity = originalVelocity;

            Debug.Log("Speed restored.");
            restoreSpeedCoroutine = null;  // Zresetuj korutynê
        }
    }
}
