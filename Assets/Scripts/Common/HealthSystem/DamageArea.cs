using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class DamageArea : MonoBehaviour
    {
        [SerializeField] DamageSystem damageSystem;
        public float amount = 0.1f; // 0-1 range
        public float frequency = 1.1f; // in seconds

        bool inRange;

        float damage;
        Vector2 damageSource;
        float damageRadius;
        private void Update()
        {
            if(inRange)
            {
                float fromSource = Vector2.Distance(transform.position, damageSource);
                float dmgScale = Mathf.InverseLerp(damageRadius, 0, fromSource);
                damage = amount * dmgScale;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Damage"))
            {
                damageSource = collision.transform.position;
                damageRadius = Vector2.Distance( damageSource, collision.bounds.max );
                inRange = true;
                StartCoroutine(TakeDamage());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Damage"))
            {
                inRange = false;
                StopAllCoroutines();
            }
        }

        IEnumerator TakeDamage()
        {
            while(inRange)
            {
                damageSystem.ApplyDrain(amount, transform);
                yield return new WaitForSeconds(frequency);
            }
        }
    }
}
