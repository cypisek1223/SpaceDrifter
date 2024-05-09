using System;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Waypoint : PoolEmitter
    {
        private WaypointManager manager;
        private int index;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if ( manager.IsCurrent(this) )
                {
                    manager.Progress();

                    ParticleSystem particle = PoolParticleManager.Instance.GetInstance(this.GetType());
                    particle.transform.position = transform.position;
                    particle.Play();
                    Destroy(gameObject); 
                }
            }
        }

        public void Init(WaypointManager waypointManager, int i)
        {
            manager = waypointManager;
            index = i;
        }
    } 
}