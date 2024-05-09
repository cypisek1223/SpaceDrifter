using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim2D
{
    // Rope Joint Factory initializes Rope Joints with initial settings
    public class JointFactory2D : MonoBehaviour
    {
        [SerializeField] JointConfig hookSetup;
        [SerializeField] JointConfig jointsSetup;
        [SerializeField] JointConfig lastJointSetup;

        [SerializeField]
        private RopeJoint2D jointPrefab;
        [SerializeField]
        private RopeJoint2D hookPrefab;

        public RopeJoint2D SpawnJoint(bool last)
        {
            RopeJoint2D newJoint = Instantiate(jointPrefab);
            newJoint.PassConfigurationData(last ? lastJointSetup : jointsSetup);
            return newJoint;
        }
        public RopeJoint2D SpawnHook()
        {
            RopeJoint2D newHook = Instantiate(hookPrefab);
            newHook.PassConfigurationData(hookSetup);
            return newHook;
        }
    }
}