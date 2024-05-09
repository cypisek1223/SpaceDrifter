using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] float impactPointDamage = 400;
        [SerializeField] HealthBehaviour health;

        [SerializeField] private Logger logger;

        public UnityEvent<Vector2, Vector2> Death; //contact and normal
        public UnityEvent<float, bool> OnDealDamage; //amount, if critical

        private void Start()
        {
            Death.AddListener(GetComponent<SpaceshipDestroyer>().DestroyShip);
        }
        private void OnDestroy()
        {
            Death.RemoveListener(GetComponent<SpaceshipDestroyer>().DestroyShip);
        }

        private bool DealDamage(float dmg01)
        {
            bool crit = health.DealDamage(dmg01);
            OnDealDamage.Invoke(dmg01, crit);
            return crit;
        }

        public void ApplyImpact(float impactForce, Vector2 contact, Vector2 normal)
        {
            logger.Log("Hit impact force: " + impactForce);

            bool crit = DealDamage(impactForce / impactPointDamage);

            if(crit)
            {
                Death?.Invoke(contact, normal);
            }
        }

        public void ApplyDrain(float healthAmount, Transform target)
        {
            logger.Log("Drain amount: " + healthAmount);

            bool crit = DealDamage(healthAmount);

            if (crit)
            {
                //Death?.Invoke(Vector2.zero, Vector2.zero);
                Death?.Invoke(target.position, target.up);
            }
        }

        public void SetHealth(float health)
        {
            this.health.Health = health;
        }
    } 
}
