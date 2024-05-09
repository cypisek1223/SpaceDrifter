using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Coin : PoolEmitter
    {
        private Type t = typeof(TextMesh);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                var particle = PoolParticleManager.Instance.GetInstance( this.GetType() );
                particle.transform.position = transform.position;
                particle.Play();

                ScoreKeeper.Instance.CoinCollect();

                Destroy(gameObject);
            }
        }
    } 
}
