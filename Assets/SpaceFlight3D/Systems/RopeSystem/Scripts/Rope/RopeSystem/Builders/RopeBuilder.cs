using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

namespace RopeMechanim2D
{
    [CreateAssetMenu]
    public class RopeBuilder : ScriptableObject, IRopeBuilder
    {
        private RopeMechanim ropeMechanim;
        private List<RopeJoint2D> joints => ropeMechanim.joints;
        private JointFactory2D factory;

        RopeJoint2D hook => joints.Find(j => j.IsHook);
        protected RopeJoint2D lastJoint => joints.Count > 0   ?  joints.Last() : null;

        #region Builder Initialization
        public virtual void ResetBuilder(RopeMechanim rope)
        {
            ropeMechanim = rope;
            factory = rope.jointFactory;

            if (joints != null)
            {
                foreach (RopeJoint2D joint in joints)
                {
                    if (joint)
                    {
                        Destroy(joint.gameObject); 
                    }
                }
                joints.Clear();
            }

            Assert.AreEqual(0, joints.Count);
        }
        #endregion

        #region Starting
        public virtual void BuildHook()
        {
            RopeJoint2D hook = factory.SpawnHook();

            ConfigureConnection(hook);
            ConfigureNeighbours(hook);
            ConfigureParent(hook);
            hook.SetDynamic();
            //ConfigurePreviousJoint(hook);

            hook.IsHook = true;
            joints.Add(hook);
        }
        public virtual void StartRollingDown()
        {
            if(hook == null)
            {
                BuildHook();
                //Debug.Break();
            }

            // A little change here so rope rolls out static and then becomes dynamic
            //hook.SetDynamic();
            //hook.SetStatic();
        }
        public virtual void StartRollingUp()
        {
            //lastJoint.SetKinematic();
        }
        #endregion

        #region Updating
        public void BuildJoint(bool last)
        {
            RopeJoint2D newJoint = factory.SpawnJoint(last);

            ConfigureConnection(newJoint);
            ConfigureNeighbours(newJoint);
            ConfigureParent(newJoint);
            ConfigurePreviousJoint(newJoint);
            newJoint.SetDynamic();
            //newJoint.SetStatic();

            joints.Add(newJoint);

        }
        public virtual bool UpdateLastJoint(float deltaPosition)
        {
            //Debug.Log($"Current anchor y: {lastJoint.Anchor.y}; DeltaPos: {deltaPosition}");
            Vector3 lastJointAnchor = lastJoint.DistanceToConnected; 

            if (lastJointAnchor.y + deltaPosition < 0)
            {
                lastJointAnchor.y = 0;
                return true;
            }
            lastJointAnchor.y += deltaPosition;
            lastJoint.SetDistanceToConnectedBody( lastJointAnchor );
            return false;
        }

        public RopeJoint2D DestroyLastJoint()
        {
            RopeJoint2D tmp = lastJoint;
            joints.Remove(lastJoint);
            Destroy(tmp.gameObject);

            if (lastJoint)
            {
                //probably needs to be enwraped in if
                lastJoint.ConnectTo(ropeMechanim.rb,
                    new Vector2(0, ropeMechanim.distanceBetweenJoints)); 
            }

            return tmp;
        }
        #endregion

        #region Finishing
        public virtual void FinishRollingDown()
        {
            //Vector3 handleFromPlacement = ropeMechanim.transform.position - lastJoint.transform.position;
            //int leftToSpawn = (int)(handleFromPlacement.magnitude / ropeMechanim.distanceBetweenJoints);
            //for (int i = 1; i <= leftToSpawn; i++)
            //{
            //    RopeJoint2 newJoint = factory.SpawnJoint();

            //    ConfigureConnection(newJoint, jointsSetup, ropeMechanim.distanceBetweenJoints * i);
            //    ConfigureRigidbody(newJoint, jointsSetup);
            //    ConfigureNeighbours(newJoint);
            //    ConfigureParent(newJoint);
            //    ConfigurePreviousJoint(newJoint);
            //    ActivateJoint(newJoint);

            //}
        }
        public virtual void FinishRollingUp()
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Joints Configuration
        protected virtual void ConfigureConnection(RopeJoint2D joint)
        {
            joint.transform.position = ropeMechanim.transform.position;
            joint.ConnectTo(ropeMechanim.rb, Vector2.down * 0.1f);
        }

        protected virtual void ConfigureNeighbours(RopeJoint2D joint)
        {
        }

        protected virtual void ConfigureParent(RopeJoint2D joint)
        {
            joint.transform.parent = null;
        }

        protected virtual void ConfigurePreviousJoint(RopeJoint2D newJoint)
        {
            if (joints.Count == 0)
                return;
            lastJoint.ConnectTo(newJoint, 
                new Vector2(0, ropeMechanim.distanceBetweenJoints));
           // lastJoint.SetDynamic();
        }
        #endregion
    }
}