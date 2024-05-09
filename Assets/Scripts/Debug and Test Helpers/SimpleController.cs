using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SimpleController : MonoBehaviour
    {
        public float speed = 1;
        public float rotSpeed = 1;

        Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 disp = new Vector2(x, y).normalized;
            disp *= speed * Time.deltaTime;
            if(rb)
            {
                //rb.MovePosition(rb.position + disp);
                rb.AddForce(disp * 55);
            }
            else
            {
                transform.Translate(disp, Space.World);
            }
        }

        private void Rotate()
        {
            float rot = -Input.GetAxis("Rotation") ;
            rot *= rotSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rot);
        }
    } 
}
