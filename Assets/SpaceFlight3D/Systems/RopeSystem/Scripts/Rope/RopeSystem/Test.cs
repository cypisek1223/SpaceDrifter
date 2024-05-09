using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim2D
{
    public class Test : MonoBehaviour
    {
        public Transform child;
        public float vertForce = 1;
        public float maxDistance = 2;
        Vector2 startPos;
        bool up;
        SpringJoint2D spring;

        private void Start()
        {
            startPos = transform.position + Vector3.down * 0.25f;
            child.position = startPos;
            child.GetComponent<SpriteRenderer>().enabled = false;
        }

        private void Update()
        {
            if(spring == null)
            {
                if(Input.GetButtonDown("Jump"))
                {
                    spring = child.gameObject.AddComponent<SpringJoint2D>();
                    spring.autoConfigureDistance = false;
                    spring.distance = 0.25f;
                    spring.connectedBody = GetComponent<Rigidbody2D>();

                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if(!up)
                {
                    spring.distance += Time.deltaTime;
                    up = spring.distance > maxDistance;
                }
                else
                {
                    spring.distance -= Time.deltaTime;
                    up = spring.distance < 0.25f;
                }
            }
        }
    }
}
