using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RopeMechanim2D
{
    public class RopeMechanim : RopeStateMachine
    {
        // Dependecies
        [SerializeField]public JointFactory2D jointFactory;
        [SerializeField]public RopeBuilder ropeBuilder;

       //Mechanics
        public Rigidbody2D rb { get; private set; }
        //public Transform ropeHandlePlacement { get; private set; }
        public Transform hook => joints.Find(j => j.IsHook)?.transform;
        public List<RopeJoint2D> joints { get; set; }
        public Transform lastJoint => joints.Last().transform;

        //Settings
        public float distanceBetweenJoints = 1;
        public float ropeLength = 2;
        public float dropSpeed = 1;
 
        public LineRenderer lr;

        public float RopeLength { get { return ropeLength; } set { ropeLength = value; } }

        void Awake()
        {
            joints = new List<RopeJoint2D>();
            rb = GetComponent<Rigidbody2D>();
        }
        public void Start()
        {
            ropeBuilder.ResetBuilder(this);
            ChangeState(new RolledUpState(this));
        }

        void Update()
        {
            //ropeStateMachine.Update();
            state.Update();
            //Debug.Log($"State is: {state.GetType()}");
            RenderLine();
        }

        private void FixedUpdate()
        {
            state.FixedUpdate();
        }

        private void RenderLine()
        {
            lr.positionCount = joints.Count + 1;
            lr.SetPosition(joints.Count, transform.position);
            int i = 0;
            foreach (RopeJoint2D j in joints)
            {
                lr.SetPosition(i, j.transform.position);
                i++;
            }
        }

        public void ToggleState()
        {
            state.Toggle();
        }
    }
}