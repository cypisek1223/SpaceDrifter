using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Engine : MonoBehaviour
    {
        #region Access Fields
        public float MaxPower { get { return maxPower; } set { maxPower = value; } }
        public float FuelUsage { get { return fuelUsagePerSecond; } set { fuelUsagePerSecond = value; } }
        public bool Push { get; set; }
        #endregion

        [SerializeField] float maxPower = 0.5f; 
        [SerializeField] float timeToMaxPower;
 

        [SerializeField] protected float power;
        [SerializeField] float fuelUsagePerSecond = 10;

        public float availableFuel;

        [SerializeField] FuelTank tank;
        Rigidbody2D rb;

        #region Unity Callbacks
        protected virtual void Start()
        {
            rb = GetComponentInParent<Rigidbody2D>();
           // particleController = GetComponentInChildren<EngineParticleController>();
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, transform.up * power / maxPower, Color.green);  
#endif

            Push = InputHandler.Instance.Thrust > 0;
            AccumulateForce();

            //Debug.Log("Prendkosc:" +rb.velocity + " magnitude:"+ rb.velocity.magnitude);
           
            if (power == maxPower)
            {
                Debug.Log("MAX POWER");
            }
        }

        private void FixedUpdate()
        {
            Thrust();
           // HandleParticle();
        }
        #endregion

        #region Adjustable Overidable Force Accumulating methods
        protected virtual void AccumulateForce()
        {
           
            if (Push)
            {

                power = Mathf.Lerp(power, maxPower, timeToMaxPower);

            }
            else
            {
                power = 0;
            }
        }
        #endregion

        #region Mechanics Methods
        public void Thrust()
        {
            Debug.LogError("THRUST");
             availableFuel = tank ? tank.UseFuel(fuelUsagePerSecond * Time.deltaTime * power / MaxPower) : fuelUsagePerSecond * Time.deltaTime * power / MaxPower;

            if (availableFuel > 0)
            {
                // Available Fuel will always be less or equal to FuelUsage over time, which means that actualPower is in (0-1) range
                float actualPower = availableFuel / (fuelUsagePerSecond * Time.deltaTime);
                Vector3 force = transform.up * power * rb.mass * actualPower;
               // rb.AddForce(force);
                rb.AddForceAtPosition(force, transform.position);
            }
        }
        #endregion
    }
}
