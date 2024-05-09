using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Gate : DoubleGate
    {
        [SerializeField] private Collider2D coll;

        protected void Start()
        {
            coll = GetComponent<Collider2D>();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if(collision.CompareTag("Player"))
            {
                sprite.color = Color.red;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            //Lock gate
            coll.isTrigger = false;
        }
    } 
}
