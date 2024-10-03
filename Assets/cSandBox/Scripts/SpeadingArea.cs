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
                // Je�li gracz jest w obszarze przyspieszenia
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
                Vector2 direction = playerRb.transform.up; // "up" wskazuje kierunek, w kt�rym patrzy statek

                // Zwi�ksz pr�dko�� tylko w kierunku obrotu
                playerRb.velocity = direction * (originalVelocity.magnitude * speedMultiplier);

                Debug.Log("BOOST");
                //Handheld.Vibrate();
            }
            else
            {
                // Je�li gracz opu�ci� obszar przyspieszenia i nadal by� boostowany
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

            // Czekaj okre�lony czas przed przywr�ceniem pr�dko�ci
            yield return new WaitForSeconds(boostDuration);

            // Wy��cz cz�steczki przyspieszenia, w��cz inne efekty
            playerController.smokeParticles.SetActive(true);
            playerController.fireParticles.SetActive(true);
            playerController.speedingParticles.SetActive(false);  // Wy��cz efekty przyspieszenia

            // Przywr�� oryginaln� pr�dko�� gracza
            playerRb.velocity = originalVelocity;

            Debug.Log("Speed restored.");
            restoreSpeedCoroutine = null;  // Zresetuj korutyn�
        }
    }
}
