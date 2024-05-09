using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Weight : MonoBehaviour
    {
        [SerializeField] float weightMultiplier = 1;
        float weight;
        Rigidbody2D rb;
        Collider2D col;

        private void Start()
        {
            CalculateWeight();
        }

        protected virtual void CalculateWeight()
        {
            if(!col)
                col = GetComponent<Collider2D>();

            if (!rb)
                rb = GetComponent<Rigidbody2D>();

            weight = col.bounds.size.x * col.bounds.size.y;
            weight *= weightMultiplier;

            rb.mass = weight;
        }
    }
}
