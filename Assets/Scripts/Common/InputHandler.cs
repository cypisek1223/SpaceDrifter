using System;
using System.Collections;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class InputHandler : Singleton<InputHandler>
    {
        [SerializeField] private GameObject panelThrustLeft;
        [SerializeField] private GameObject panelThrustRight;

        [SerializeField] private HoldableDoublePressButton thrustButton;
        [SerializeField] private HoldableButton leftButton;
        [SerializeField] private HoldableButton rightButton;

        [SerializeField] private float turboTime = 2;
        [SerializeField] private float turboCooldown = 5;
        private bool turbo;
        private bool cooldown;
        public static Action TurboThrustActivated;
        public static Action TurboThrustDeactivated;

        public bool MouseDown { get { return mouseDown; } }
        public float Thrust { get { return thrust; } }
        public float LeftRight { get { return leftRight; } }

        protected bool mouseDown;
        protected float thrust;
        protected float leftRight;

        //CYPRIAN ADDED THIS 
        [SerializeField] PlayerController playerController;
        [SerializeField]  bool fisrtTime = true;
        protected virtual void Start()
        {
            thrustButton.OnDoublePress += TurboOn;
            thrustButton.OnDoubleLeft += TurboOff;
        }

        protected virtual void OnDestroy()
        {
            thrustButton.OnDoublePress -= TurboOn;
            thrustButton.OnDoubleLeft -= TurboOff;
        }

        private void TurboOn()
        {
            if (cooldown) return;

            if (fisrtTime || playerController.renewParticles)
            {
                Debug.Log("SMOKE ON");
                playerController.partilesController.enabled = true;
                playerController.smokeParticles.SetActive(true);
                playerController.fireParticles.SetActive(true);

                playerController.renewParticles = false;
                fisrtTime = false;
            }

            turbo = true;
            TurboThrustActivated?.Invoke();
            StartCoroutine(TurboTimer());
        }
        private void TurboOff()
        {
            if(turbo)
            {
                TurboThrustDeactivated?.Invoke();
                StartCoroutine(TurboCooldown());
            }
            turbo = false;
        }
        private IEnumerator TurboCooldown()
        {
            cooldown = true;
            yield return new WaitForSeconds(turboCooldown);
            cooldown = false;
        }

        private IEnumerator TurboTimer()
        {
            yield return new WaitForSeconds(turboTime);
            TurboOff();
        }

        protected virtual void Update()
        {
           // UpdateMouseDown(); //Consider this if is needed
            UpdateUpwards();
            UpdateLeftRight();

            if (isSlowingDown && thrust <=0)
            {
                ApplySlowdown();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                isSlowingDown = true;
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                isSlowingDown = false;
            }
        }

//External methods
        public void Deactivate()
        {
            gameObject.SetActive(false);
            panelThrustLeft.SetActive(false);
            panelThrustRight.SetActive(false);

            mouseDown = false;
            thrust = 0;
            leftRight = 0;
        }
        public void Activate()
        {
            gameObject.SetActive(true);
                
            //Scriptable object with settings wheter right/left needs to be introduced
            panelThrustRight.SetActive(true);
        }

//Internal methods
        protected virtual void UpdateUpwards()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Space))
            {
                thrust = 1;
               if (fisrtTime||playerController.renewParticles)
               {
                    Debug.Log("SMOKE ON");
                    playerController.partilesController.enabled = true;
                    playerController.smokeParticles.SetActive(true);
                    playerController.fireParticles.SetActive(true);

                    playerController.renewParticles = false;
                    fisrtTime = false;
               }
            }
            else
            {
                thrust = 0;
            }
#else
            if(thrustButton.IsHeld)
            {
            thrust = 1;
            }
            else
            {
            thrust = 0;
            }
#endif  
        }
        //public void Smoke()
        //{
        //    fisrtTime = true;
        //}
        protected virtual void UpdateMouseDown()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mouseDown = true;
            }
            else
            {
                mouseDown = false;
            }
#else
        if(Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
#endif
        }
        protected virtual void UpdateLeftRight()
        {
#if UNITY_EDITOR
            leftRight = Input.GetAxis("Horizontal");
#else
            leftRight = (leftButton.IsHeld ? -1 : 0) + (rightButton.IsHeld ? 1 : 0);
#endif
        }





        #region Slow Down
        public Rigidbody2D rb;           
        public float slowdownForce = 5f; 
        public float slowdownFactor = 0.5f;
        public bool isSlowingDown = false;

        //IT WILL BE CHANGING
        public SpeedingArea speedingArea;
        public void StartSlowdown()
        {
            isSlowingDown = true;
        }
        public void StopSlowdown()
        {
            isSlowingDown = false;
        }

        private void ApplySlowdown()
        {
            if (rb.velocity.magnitude > 1.2f && speedingArea.boosting == false)
            {
                Vector2 slowdown = Vector2.down * slowdownForce;
                rb.AddForce(slowdown, ForceMode2D.Force);
                //rb.velocity = rb.velocity * slowdownFactor;
            }
        }
        #endregion
    }

}