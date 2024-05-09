using System;
using UnityEngine;

namespace RopeMechanim2D
{
    [CreateAssetMenu(fileName = "DistanceJointConfiguration", menuName = "Joint Configuration/Distance Joint Configuration", order = 1)]
    public class DistanceSetup : JointConfig
    {
        [Header("Distance Joint Specific")]
        public bool autoConfigureDistance;
        public float distance = 0;

        public override Type GetJointType()
        {
            return typeof(DistanceJoint2D);
        }

        public override Vector2 GetDistance(Joint2D joint)
        {
            DistanceJoint2D j = joint as DistanceJoint2D;
            //return j.anchor;
            return new Vector2(0, j.distance);
        }

        public override void SetDistance(Joint2D joint, Vector2 distance)
        {
            DistanceJoint2D j = joint as DistanceJoint2D;
            //j.anchor = anchor;
            j.distance = distance.magnitude;
        }

        public override void ConfigureJoint(Joint2D joint)
        {
            DistanceJoint2D j = joint as DistanceJoint2D;

            j.autoConfigureConnectedAnchor = this.autoConfigure;

            if (!j.autoConfigureConnectedAnchor)
            {
                j.connectedAnchor = connectedAnchor;
            }

            j.autoConfigureDistance = autoConfigureDistance;
            if(!j.autoConfigureDistance)
            {
                j.distance = this.distance;
            }
        }
    }
}
