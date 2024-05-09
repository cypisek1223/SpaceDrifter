using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class GroundParticleEffect : MonoBehaviour
    {
        public LayerMask effectMask;
        public Transform effect;
        public float distance;

        //Config settings
        [SerializeField] private float maxRateOverTime_thrust = 45;
        [SerializeField] private float maxRateOverTime = 25;

        [SerializeField] private float maxRadius_thrust = 2f;
        [SerializeField] private float maxRadius = 2f;

        [SerializeField] private float maxStartSize_thrust = 4f;
        [SerializeField] private float maxStartSize = 4f;


        ParticleSystem ps;
        ParticleSystem.MainModule pmm;
        ParticleSystem.EmissionModule pem;
        ParticleSystem.ShapeModule psm;

        private void Start()
        {
            ps = effect.GetComponent<ParticleSystem>();
            pem = ps.emission;
            psm = ps.shape;
            pmm = ps.main;

        }

        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, distance, effectMask);
            if (hit.collider != null)
            {
                effect.position = hit.point + hit.normal * 0.1f;
                effect.up = hit.normal;
                if(!ps.isPlaying)
                {
                    ps.Play();
                }
                float dist01 = Mathf.Clamp(hit.distance / distance, 0, 1);

                //pem.rateOverTimeMultiplier = (1 - dist01) * maxRateOverTime_thrust;
                pem.rateOverTimeMultiplier = maxRateOverTime_thrust;
                psm.radius = (dist01) * maxRadius_thrust;
                //pmm.startSize = (1 - dist01) * maxStartSize_thrust;
                pmm.startSize = maxStartSize_thrust;

                if (InputHandler.Instance.Thrust == 0)
                {
                    //pem.rateOverTimeMultiplier = (1 - dist01) * maxRateOverTime;
                    pem.rateOverTimeMultiplier = maxRateOverTime;
                    psm.radius = (dist01) * maxRadius;
                    //pmm.startSize = (1 - dist01) * maxStartSize;
                    pmm.startSize = maxStartSize;
                }
            }
            else
            {
                ps.Stop();
            }
        }
    }

}