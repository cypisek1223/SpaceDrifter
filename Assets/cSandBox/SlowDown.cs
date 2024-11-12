using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{

    public class SlowDown : MonoBehaviour
    {
        
        public Rigidbody2D rb;           // Rigidbody gracza
        public float slowdownForce = 5f; // Si³a wsteczna przy zwalnianiu
        public float slowdownFactor = 0.5f;
        public bool isSlowingDown = false;

        //IT WILL BE CHANGING
        public SpeedingArea speedingArea;

        private void Update()
        {
            if (isSlowingDown)
            {
                ApplySlowdown();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("SLOW DOWWN");
                isSlowingDown = true;
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                isSlowingDown = false;
            }
        }

        // Wywo³ywane, gdy przycisk jest przytrzymany
        public void StartSlowdown()
        {
            isSlowingDown = true;
        }

        // Wywo³ywane, gdy przycisk jest zwolniony
        public void StopSlowdown()
        {
            isSlowingDown = false;
        }

        private void ApplySlowdown()
        {
            if (rb.velocity.magnitude > 1.2f && speedingArea.boosting == false)
            {
                //Debug.Log("Zwalanianie status");
                Vector2 slowdown = Vector2.down * slowdownForce;
                rb.AddForce(slowdown, ForceMode2D.Force);
                //rb.velocity = rb.velocity * slowdownFactor;
            }
        }
    }
}

