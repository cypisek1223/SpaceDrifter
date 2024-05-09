using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim2D
{
    internal class RollingDownState : RopeState
    {
        Transform hook => ropeMechanim.hook;
        Transform lastJoint => ropeMechanim.lastJoint;
        Transform ropeHandle => ropeMechanim.transform;
        float ropeLength => ropeMechanim.ropeLength;
        float distanceBetweenJoints => ropeMechanim.distanceBetweenJoints;

        int jointsToSpawn;

        public RollingDownState(RopeMechanim ropeMechanim) : base(ropeMechanim) { }

        public override void OnEnter()
        {
            ropeMechanim.ropeBuilder.StartRollingDown();
            //hook.position = Vector2.down * 0.01f;

            jointsToSpawn = (int)Mathf.Ceil(ropeLength / distanceBetweenJoints);
        }

        public override void FixedUpdate()
        {
            //Finish rollin down
            if (jointsToSpawn <= 0)
            {
                ropeMechanim.ropeBuilder.FinishRollingDown();
                ropeMechanim.ChangeState(new RolledDownState(ropeMechanim));
                return;
            }

            //Roll last joint down or spawn new if reached target distance
            float lastToHandle = Vector3.Distance(lastJoint.position, ropeHandle.position);
            if (lastToHandle + Time.unscaledDeltaTime * ropeMechanim.dropSpeed < ropeMechanim.distanceBetweenJoints)
            {
                ropeMechanim.ropeBuilder.UpdateLastJoint(Time.unscaledDeltaTime * ropeMechanim.dropSpeed);
            }
            else
            {
                ropeMechanim.ropeBuilder.BuildJoint(jointsToSpawn == 1);
                jointsToSpawn--;
            }
        }
    }
}