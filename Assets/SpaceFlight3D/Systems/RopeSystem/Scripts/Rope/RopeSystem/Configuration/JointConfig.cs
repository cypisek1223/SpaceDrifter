using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim2D
{

    //[CreateAssetMenu(fileName = "JointConfiguration", menuName = "ScriptableObjects/Static Configurations/Hinge Joint Configuration", order = 1)]
    public abstract class JointConfig : ScriptableObject
        //where T : Joint
    {
        public virtual Type GetJointType()
        {
            return typeof(Joint2D);
        }

        [TextArea]
        public string description;

        [Header("Rigidbody Configuration")]
        public int layer = 0;
        public bool isKinematic;
        public float mass = 1;
        public float drag;

        public float angularDrag;
        public RigidbodyInterpolation2D interpolate;

        [Tooltip("Constraints Sum Up")]
        public RigidbodyConstraints2D constraints1;
        public RigidbodyConstraints2D constraints2;
        public RigidbodyConstraints2D constraints3;


        [Header("Joint Configuration")]
        public bool autoConfigure;
        public Vector2 connectedAnchor;

        [Header("Additional")]
        public float pushDownForce;


        public virtual Vector2 GetDistance(Joint2D joint)
        {
            //Need to cast joint to specific 2d joint in specific jointCOnfig deriving classes.
            //This means that JointConfig class itself must be generic and abstract not to be calling this
            throw new NotImplementedException();
        }
        public virtual void SetDistance(Joint2D joint, Vector2 distance)
        {
            //Similar to GetDistance
            throw new NotImplementedException();
        }


        public virtual void ConfigureAdditional(GameObject jointObject)
        {
            ConstantForce2D cf = jointObject.GetComponent<ConstantForce2D>();
            if(cf)
            {
                cf.force = new Vector2(0, -pushDownForce);
            }
        }

        public virtual void ConfigureRigidbody(Rigidbody2D rb)
        {
            rb.gameObject.layer = layer;

            //Rigidbody rb = joint.rb;
            rb.interpolation = this.interpolate;

            rb.drag = this.drag;
            rb.angularDrag = this.angularDrag;
            rb.mass = this.mass;
            rb.constraints = this.constraints1 | this.constraints2 | this.constraints3;
        }

        public virtual void ConfigureJoint(Joint2D j)
        {
            //j.autoConfigureConnectedAnchor = this.autoConfigure;
            //j.massScale = this.massScale;
            //j.connectedMassScale = this.connectedMassScale;

            //if (!j.autoConfigureConnectedAnchor)
            //{
            //    j.axis = this.axis;
            //    j.connectedAnchor = connectedAnchor;
            //    //j.anchor = Vector3.zero;
            //}
        }

        //public virtual void ConfigureAnchor(RopeJoint2 joint)
        //{
        //    Joint j = joint.joint;

        //    if (!j.autoConfigureConnectedAnchor)
        //    {
        //        j.anchor = this.anchor / j.transform.localScale.y;
        //        j.axis = this.axis;
        //        j.connectedAnchor = connectedAnchor;
        //    }
        //}
    }
}