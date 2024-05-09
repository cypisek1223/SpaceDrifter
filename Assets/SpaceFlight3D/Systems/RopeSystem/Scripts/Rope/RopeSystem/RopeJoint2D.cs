using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace RopeMechanim2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RopeJoint2D : MonoBehaviour
    {
        private bool configured;

        public Rigidbody2D Rb { get; private set; }
        public Joint2D Joint { get; private set; }

        public bool IsHook { get; set; }
        public RopeJoint2D UpperOne { get; set; }
        public RopeJoint2D LowerOne { get; set; }

        private JointConfig configData;

        public Vector2 DistanceToConnected
        {
            get => Vector2.Scale(configData.GetDistance(Joint), transform.localScale);
            private set => configData.SetDistance( Joint, Vector2.Scale(value, new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y)));
        }

        public void PassConfigurationData(JointConfig jointConfig)
        {
            configData = jointConfig;
        }

        public static implicit operator bool(RopeJoint2D j) => j != null;
        //public static implicit operator Transform(RopeJoint2 j) => j.rb.transform;
        public static bool operator true(RopeJoint2D j) => j != null;
        public static bool operator false(RopeJoint2D j) => j == null;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
        }

        public void SetDistanceToConnectedBody(Vector2 anchor)
        {
            Assert.IsTrue(configured);
            DistanceToConnected = anchor;
        }

        public void SetDynamic()
        {
            //Rb.isKinematic = false;
            Rb.bodyType = RigidbodyType2D.Dynamic;
        }
        public void SetStatic()
        {
            //Rb.isKinematic = true;
            Rb.bodyType = RigidbodyType2D.Kinematic;
        }
        public void Disconnect()
        {
            if (Joint != null)
            {
                Destroy(Joint);
            }
        }

        public void ConnectTo(RopeJoint2D upperJoint, Vector2 anchor)
        {
            ConnectTo(upperJoint.Rb, anchor);
            UpperOne = upperJoint;
            upperJoint.LowerOne = this;
        }
        public void ConnectTo(Rigidbody2D rigidbody, Vector2 anchor)
        {
            if (Joint != null)
            {
                Destroy(Joint);
            }

            if (configData == null)
            {
                //Joint = this.gameObject.AddComponent<Joint2D>();
                //SHould actually fill in some default data
                Debug.LogError("Rope Joint not initialised properly: " + name);
            }
            else
            {
                Joint = this.gameObject.AddComponent(configData.GetJointType()) as Joint2D;
                configData.ConfigureJoint(Joint);
                configData.ConfigureRigidbody(Rb);
                configData.ConfigureAdditional(gameObject);
            }

            Joint.connectedBody = rigidbody;
            configured = true;
            SetDistanceToConnectedBody(anchor);

            //Debug.Log(anchor);
            //Debug.Break();
        }
    }
}