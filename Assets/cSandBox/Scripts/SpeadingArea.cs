using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SpeedingArea : MonoBehaviour
    {
        private Vector2 originalVelocity;
        [SerializeField] Vector2 boostVelocity;
        private bool boosting = false;
        private Coroutine speedDecayCoroutine;

        [SerializeField] Rigidbody2D playerRb;
        [SerializeField] PlayerController playerController;

        [SerializeField] float speedMultiplier = 1.2f;
        [SerializeField] float boostDuration = 2f;
        [SerializeField] float detectionRadius = 0.5f;

        [SerializeField] float speedChangeRate = 0.5f; 
        [SerializeField] LayerMask boostSpeed;

        private float boostTimer = 0f;
        private Vector2 targetVelocity;

        private void Update()
        {
            OnAreaBoost();
        }

        private void OnAreaBoost()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, boostSpeed);

            if (colliders.Length > 0)
            {
                if (!boosting)
                {
                    boosting = true;
                    if (speedDecayCoroutine != null)
                    {
                        StopCoroutine(speedDecayCoroutine);
                        speedDecayCoroutine = null;
                    }

                    playerController.smokeParticles.SetActive(false);
                    playerController.fireParticles.SetActive(false);
                    playerController.speedingParticles.SetActive(true);

                    originalVelocity = playerRb.velocity;

                    Vector2 direction = playerRb.transform.up;
                    targetVelocity = direction * (originalVelocity.magnitude * speedMultiplier);  
                }


                playerRb.velocity = Vector2.Lerp(playerRb.velocity, targetVelocity, speedChangeRate * Time.deltaTime);

            }
            else
            {
                if (boosting && speedDecayCoroutine == null)
                {
                    speedDecayCoroutine = StartCoroutine(GradualSpeedDecay());
                }
            }
        }

        private IEnumerator GradualSpeedDecay()
        {          
            yield return new WaitForSeconds(boostDuration);

            boosting = false;

            playerController.smokeParticles.SetActive(true);
            playerController.fireParticles.SetActive(true);
            playerController.speedingParticles.SetActive(false);

            yield return null;
        }
    }
}
