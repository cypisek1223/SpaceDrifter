using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D Rb => rb;
        public float NormalizedSpeed => Rb.velocity.sqrMagnitude / sqrMaxSpeed;

        [SerializeField] float sqrMaxSpeed = 450;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer ship;
        [SerializeField] private SpriteRenderer shipInnerGlow;
        [SerializeField] private EnginesParticleController engineParticles;
        [SerializeField] public GameObject smokeParticles;
        [SerializeField] public GameObject fireParticles;
        [SerializeField] public GameObject speedingParticles;



        [SerializeField] DamageSystem damageSys;

        private Engine[] engines;
        [SerializeField] private float increasedEnginesPower = 11;
        private float enginesPower;

        private void Start()
        {
            engines = GetComponentsInChildren<Engine>(true);
            enginesPower = engines[0].MaxPower;

            InputHandler.TurboThrustActivated += IncreaseEnginesPower;
            InputHandler.TurboThrustDeactivated += ReduceEnginesPower;
        }

        private void OnDestroy()
        {
            InputHandler.TurboThrustActivated -= IncreaseEnginesPower;
            InputHandler.TurboThrustDeactivated -= ReduceEnginesPower;
        }

        public void IncreaseEnginesPower()
        {
            foreach(var e in engines)
            {
                e.MaxPower = increasedEnginesPower;
            }
        }
        public void ReduceEnginesPower()
        {
            foreach (var e in engines)
            {
                e.MaxPower = enginesPower;
            }
        }

        public void Kill()
        {
            Pause();
            Hide();
        }
        public void Respawn(StartSpot exit, float health)
        {
            damageSys.SetHealth(health);
            SetStartPosition(exit.startPos, exit.startRot);
          //  Unpause();
            Show();
        }
        public void TurnEnginesOff()
        {
            engineParticles.gameObject.SetActive(false);


            //smokeParticles.SetActive(false);
            //fireParticles.SetActive(false);
            //speedingParticles.SetActive(false);
        }
        public void Hide()
        {
            ship.enabled = false;
            shipInnerGlow.enabled = false;
            engineParticles.gameObject.SetActive(false);


            smokeParticles.SetActive(false);
            fireParticles.SetActive(false);
            //speedingParticles.SetActive(false);
        }
        public void Show()
        {
            ship.enabled = true;
            shipInnerGlow.enabled = true;
            engineParticles.gameObject.SetActive(true);

            smokeParticles.SetActive(true);
            fireParticles.SetActive(true);
            //speedingParticles.SetActive(false);
        }

        public void SetInnerGlowColor(Color color)
        {
            shipInnerGlow.color = color;
        }

        public void Pause()
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        public void Unpause()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        public void SetStartPosition(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            rb.transform.localPosition = Vector3.zero;

            transform.rotation = rotation;
            rb.transform.localRotation = Quaternion.identity;
        }
    } 
}
