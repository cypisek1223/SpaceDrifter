using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine;
namespace SpaceDrifter2D
{
    public class CoinCollision : PoolEmitter
    {

        private Type t = typeof(TextMesh);

        public LayerMask coinLayer;

        public float detectionRadius = 0.5f;

        //OPTIONAL
        public Transform detectionPoint;

        private void Update()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRadius, coinLayer);

            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("Coin collected");
                //Destroy(hitCollider.gameObject);
                //hitCollider.gameObject.active = false;

                //var particle = PoolParticleManager.Instance.GetInstance(this.GetType());
                //particle.transform.position = transform.position;
                //particle.Play();

                ScoreKeeper.Instance.CoinCollect();

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
