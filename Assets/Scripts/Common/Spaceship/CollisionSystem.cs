using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class CollisionSystem : MonoBehaviour
    {

        [SerializeField] private float bounceMultiplier = 1;
        [SerializeField] private float preservationMultiplier = 1;

        [SerializeField] private ParticleSystem hitGroundEffect;

        [SerializeField] private Logger logger;
        [SerializeField] private DebugDrawer debugDrawer;

        public UnityEvent<float, Vector2, Vector2> OnCollision; // force applied, contact, and normal
        public UnityEvent<Vector2, Vector2> OnTrigger; // contactPoint and normal

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                Vector2 contact = collision.contacts[0].point;
                Vector2 contactNormal = collision.contacts[0].normal;
                Vector2 vel = collision.relativeVelocity;
                float angleMultiplier = Mathf.Abs(Vector2.Dot(contactNormal, vel)); //the harder angle the more force is applied
                Vector2 force = contactNormal * angleMultiplier;

                //https://en.wikipedia.org/wiki/Specular_reflection
                Vector2 preservedVelocity = -vel - 2 * contactNormal * (Vector2.Dot(contactNormal, -vel)); // there is negative vel, since it's relative velocity

                Vector2 impactSmoothingForce = force + preservedVelocity * preservationMultiplier;

                force *= bounceMultiplier;

                rb.AddForceAtPosition(impactSmoothingForce, contact, ForceMode2D.Impulse);

                logger.Log( $"{gameObject.name} hit a wall and bounces: {impactSmoothingForce.normalized} ({impactSmoothingForce.magnitude})");
                StartCoroutine(debugDrawer.DrawRay(contact, impactSmoothingForce / bounceMultiplier, 2));
                StartCoroutine(debugDrawer.DrawRay(contact, preservedVelocity * preservationMultiplier * bounceMultiplier, 2, Color.yellow));
                StartCoroutine(debugDrawer.DrawRay(contact, contactNormal, 2, Color.white));

                //just for a sec
                hitGroundEffect.transform.position = contact;
                hitGroundEffect.transform.up = contactNormal;
                hitGroundEffect.Play();

                OnCollision.Invoke(force.magnitude, contact, contactNormal);
               // damageSystem.ApplyImpact(force.magnitude);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Deadly"))
            {
                var contactPoint = collision.ClosestPoint(rb.position);
                var normal = ((Vector2)rb.position - contactPoint).normalized;

                OnTrigger.Invoke( contactPoint, normal);
            }
        }

    } 
}
