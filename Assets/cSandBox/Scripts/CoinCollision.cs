using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class CoinCollision : PoolEmitter
    {
        [SerializeField] LayerMask coinLayer;
        [SerializeField] LayerMask bonuscCoinLayer;

        

        [Header("Coin Collecting")]
        private Type t = typeof(TextMesh);

        [SerializeField] float detectionRadius = 0.5f;

        [SerializeField] Animator coinCollectedAnim;
        [SerializeField] bool isAnimationComplete;

        [Header("Coin Magnet")]
        [SerializeField] float magnetRadius = 1f;
        [SerializeField] float magnetForce = 10f;

        [Header("Optional")]
        [SerializeField] Transform detectionPoint;

        [Header("Fuel Coin")]
        public UnityEvent fuelEvent;

        private void Update()
        {
            AttractingCoin();
            CoinsCollecting();
        }
        private void AttractingCoin()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(detectionPoint.position, magnetRadius, coinLayer);

            foreach (var collider in colliders)
            {
                //Co robi normalized
                Vector2 direction = (transform.position - collider.transform.position).normalized;

                Vector2 newPosition = Vector2.MoveTowards(collider.transform.position, transform.position, magnetForce * Time.fixedDeltaTime);

                collider.transform.position = newPosition;
            }         

        }
        private void CoinsCollecting()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRadius, coinLayer);

            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.tag == "BonusCoin")
                {
                    ScoreKeeper.Instance.BonusCoinCollect();
                }
                else if (hitCollider.tag == "FuelCoin")
                {
                    fuelEvent.Invoke();
                }
                else
                {  
                    StartCoroutine(MoveCoinToTarget());
                }
                             
                if(coinCollectedAnim != null)
                coinCollectedAnim.Play("coinCollected");
                
                Destroy(hitCollider.gameObject);

                //var particle = PoolParticleManager.Instance.GetInstance(this.GetType());
                //particle.transform.position = transform.position;
                //particle.Play();
            }
        }

        public Transform targetUI;
        public GameObject animatedCoinPrefab;
        public float animationDuration = 5f;
        public Animator coinColletingAnim;
        private IEnumerator MoveCoinToTarget()
        {

            GameObject animatedCoin = Instantiate(animatedCoinPrefab, transform.position, Quaternion.identity);

            Vector3 startPosition = animatedCoin.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                animatedCoin.transform.position = Vector3.Lerp(startPosition, targetUI.position, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null; 
            }


            animatedCoin.transform.position = targetUI.position;

            //Dodaj
            coinColletingAnim.Play("CoinAnimation");
            ScoreKeeper.Instance.CoinCollect();
            Destroy(animatedCoin); 
        }
        private void OnDrawGizmos()
        {
            if (detectionPoint == null)
            {
                detectionPoint = transform;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
        }

    }
}
