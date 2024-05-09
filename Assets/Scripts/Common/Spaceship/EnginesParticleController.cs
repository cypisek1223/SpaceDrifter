using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class EnginesParticleController : MonoBehaviour
    {
        public ParticleSystem[] engines;
        public float thrustSpeedZ = -10;
        public float steadySpeedZ = -2;
        public float thrustRate = 100;
        public float steadyRate = 10;

        bool thrusting;
        ParticleSystem.VelocityOverLifetimeModule[] velocityModules;
        ParticleSystem.EmissionModule[] emissionModules;
        ParticleSystemRenderer[] rendererModules;

        //Initialize PS modules not to retrieve them every frame
        private void Start()
        {
            velocityModules = new ParticleSystem.VelocityOverLifetimeModule[engines.Length];
            for (int i = 0; i < engines.Length; i++)
            {
                velocityModules[i] = engines[i].velocityOverLifetime;
            }

            emissionModules = new ParticleSystem.EmissionModule[engines.Length];
            for (int i = 0; i < engines.Length; i++)
            {
                emissionModules[i] = engines[i].emission;
            }

            rendererModules = new ParticleSystemRenderer[engines.Length];
            for (int i = 0; i < engines.Length; i++)
            {
                rendererModules[i] = engines[i].GetComponent<ParticleSystemRenderer>();
            }
        }

        private void Update()
        {
            if(InputHandler.Instance.Thrust > 0)
            {
                Thrust();
            }
            else
            {
                Steady();
            }
        }

        // called repeatadly
        public void Thrust()
        {
            if (!thrusting)
            {
                thrusting = true;

                for (int i = 0; i < engines.Length; i++)
                {
                    velocityModules[i].speedModifier = thrustSpeedZ;
                    emissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(thrustRate);
                }
            }
        }


        public void Steady()
        {
            if (thrusting)
            {
                thrusting = false;

                for (int i = 0; i < engines.Length; i++)
                {
                    velocityModules[i].speedModifier = steadySpeedZ;
                    emissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(steadyRate);
                }
            }
        }
    } 
}
