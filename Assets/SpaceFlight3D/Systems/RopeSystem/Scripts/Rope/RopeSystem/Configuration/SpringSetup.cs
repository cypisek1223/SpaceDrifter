using System;
using UnityEngine;

namespace RopeMechanim2D
{
    [CreateAssetMenu(fileName = "SpringJointConfig", menuName = "Joint Configuration/Spring Joint Configuration", order = 1)]
    public class SpringSetup : JointConfig
    {
        [Header("Spring Joint Specific")]
        public float frequency = 2;
        public float damping = 0.5f;

        public override Type GetJointType()
        {
            return typeof(SpringJoint2D);
        }

        public override Vector2 GetDistance(Joint2D joint)
        {
            SpringJoint2D j = joint as SpringJoint2D;
            //return j.anchor;
            return new Vector2(0, j.distance);
        }

        public override void SetDistance(Joint2D joint, Vector2 distance)
        {
            SpringJoint2D j = joint as SpringJoint2D;
            //j.anchor = anchor;
            j.distance = distance.magnitude;
        }

        public override void ConfigureJoint(Joint2D joint)
        {
            SpringJoint2D j = joint as SpringJoint2D;

            j.autoConfigureDistance = false;

            j.autoConfigureConnectedAnchor = this.autoConfigure;

            if (!j.autoConfigureConnectedAnchor)
            {
                j.connectedAnchor = connectedAnchor;
            }

            j.frequency = frequency;
            j.dampingRatio = damping;
        }
    }
}
