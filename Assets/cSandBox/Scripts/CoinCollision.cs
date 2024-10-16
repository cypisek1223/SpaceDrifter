using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
                //Destroy(hitCollider.gameObject);
                //hitCollider.gameObject.active = false;

                //var particle = PoolParticleManager.Instance.GetInstance(this.GetType());
                //particle.transform.position = transform.position;
                //particle.Play();

                if(hitCollider.tag == "BonusCoin")
                {
                    ScoreKeeper.Instance.BonusCoinCollect();
                }
                else
                {
                    ScoreKeeper.Instance.CoinCollect();
                }
                
               
                if(coinCollectedAnim != null)
                coinCollectedAnim.Play("coinCollected");
                
                Destroy(hitCollider.gameObject);
            }
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
