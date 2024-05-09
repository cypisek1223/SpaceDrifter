using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Fluffy : MonoBehaviour
    {
        public float maxForce = 1;

        Rigidbody2D m_rb;
        Rigidbody2D target_rb;
        CircleCollider2D m_collider;
        FrictionJoint2D joint;

        float max_distance;

        private void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<CircleCollider2D>();
            max_distance = m_collider.radius;

            //joint = GetComponent<FrictionJoint2D>();
        }

        Vector2 contact;
        public DebugDrawer drawer;
        [Tooltip("In meters per second")]
        float attractSpeed = 3.2f;
        public AnimationCurve attractionEase;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                target_rb = collision.GetComponent<Rigidbody2D>();
                contact = collision.ClosestPoint(transform.position);
                Debug.Log("Contact: " + contact);
                float distance = Vector2.Distance(transform.position, contact);
                float t = distance / attractSpeed;
                LeanTween.move(gameObject, contact, t).setEase(attractionEase).setOnComplete(Stick);
                StartCoroutine(drawer.DrawDot(contact, 2f, 3));
            }
        }
        public float physicalRadius = 0.2f;
        public LayerMask stickMask;
        void Stick()
        {
            if (Physics2D.OverlapCircle(transform.position, physicalRadius, stickMask) == null) return;

            //just got stick to the player
            Vector2 anchor = transform.InverseTransformPoint(contact);

            joint = gameObject.AddComponent<FrictionJoint2D>();
            joint.maxForce = 0.05f;
            joint.breakForce = 0.1f;
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = anchor;
            joint.connectedBody = target_rb;

            Debug.Break();
        }
        //private void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (!target_rb) return;

        //    if (collision.CompareTag("Player"))
        //    {
        //        Vector2 toTarget = target_rb.position - m_rb.position;
        //        float dist = toTarget.magnitude;
        //        toTarget = toTarget.normalized;

        //        float multiplier = max_distance * 1.0f - dist;
        //        multiplier /= max_distance;
        //        m_rb.AddForce(toTarget * multiplier * maxForce, ForceMode2D.Force); 
        //    }
        //}
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                target_rb = null; 
            }
        }
    }
}
