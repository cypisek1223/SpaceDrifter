using RopeMechanim2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RopeMechanim2D
{
    public class RopeHook : MonoBehaviour
    {
        [SerializeField] JointConfig jointConfig;
        Rigidbody2D package;
        RopeJoint2D ropeJoint;

        private void Awake()
        {
            ropeJoint = gameObject.AddComponent<RopeJoint2D>();
            //ropeJoint.SetStatic();
            ropeJoint.PassConfigurationData(jointConfig);
            //rb = GetComponent<Rigidbody2D>();
        }

        bool disconnecting;
        private void Update()
        {
            if (!disconnecting && Input.GetKeyDown(KeyCode.M) && package != null)
            {
                ropeJoint.Disconnect();
                disconnecting = true;
                Invoke(nameof(ResetPackage), 3);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Package") && !package)
            {
                package = other.GetComponent<Rigidbody2D>();
                ropeJoint.ConnectTo(package, Vector2.zero);
                //ropeJoint.SetDynamic();
                package.bodyType = RigidbodyType2D.Dynamic;

                //package = other.gameObject.GetComponent<Rigidbody2D>();
                //package.bodyType = RigidbodyType2D.Dynamic;
                //Joint2D joint = package.gameObject.AddComponent<DistanceJoint2D>();
                //joint.connectedBody = rb;

            }
        }

        private void ResetPackage()
        {
            package = null;
            disconnecting = false;
        }

        public void ResetPickup()
        {
            //var joint = package.GetComponent<HingeJoint2D>();
            //if (joint) Destroy(joint);

        }
    } 
}
