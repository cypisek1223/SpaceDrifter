using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDrifter2D
{
    public class Respawner : MonoBehaviour
    {
        public Vector2 Position => PickExit().transform.position;

        private static List<StartSpot> currentLevelEntraceList = new List<StartSpot>();

        [SerializeField] private PlayerController playerController;

        

        // Jump out of respawn settings
        [SerializeField] private Image powerImage;
        [SerializeField] private float jumpDuration = 7;
        [SerializeField] private float jumpDistance = 4;
        [SerializeField] private float jumpMaxPower = 10;

        [SerializeField] ParticleSystem spawnEffect;
        [SerializeField] GameObject smokeEffect;
        [SerializeField] GameObject fireEffect;

        [Header("Start Portal")]
        [SerializeField] GameObject startPortal;
        [SerializeField] Animator portalAnimation;

        private EntrancePicker respawnPicker;

        //LeanTween controls
        private int rotateId;
        private int moveId;
        private int powerId;
        private int colorId;
        private float power;

        private void Awake()
        {
            respawnPicker = ScriptableObject.CreateInstance<EntrancePicker>();
        }
        private void Start()
        {
            powerImage.enabled = false;
            //startPortal.SetActive(true);
        }

        private StartSpot PickExit()
        {
            return respawnPicker.PickEntrance(currentLevelEntraceList);
        }

        public void Respawn()
        {
            StartSpot exit = PickExit(); // Need to rethink wheter it picks everytime or picks once
            playerController.Respawn(exit, 1);


            //rotateId = LeanTween.rotateAround(playerController.Rb.gameObject, Vector3.forward, 360, 1).setRepeat(-1).setOnUpdate(Rotating).id;
            //moveId = LeanTween.move(playerController.Rb.gameObject, exit.transform.position + exit.transform.up * jumpDistance, jumpDuration).setEase(LeanTweenType.easeOutQuint).setOnStart(SpawnEffect).id;
            power = jumpMaxPower * playerController.Rb.mass * 2f;
            //powerImage.enabled = true;
            //powerId = LeanTween.value(power, 0, jumpDuration).setEase(LeanTweenType.easeOutQuint).setOnUpdate((v) => { power = v; powerImage.fillAmount = v/(jumpMaxPower * playerController.Rb.mass); }).id;
            //powerImage.color = Color.red;
            //colorId = LeanTween.color(powerImage.rectTransform, Color.white, jumpDuration).id;
            LeanTween.alpha(powerImage.rectTransform, 0.5f, 0.1f).setRepeat(7).setEase(LeanTweenType.easeInOutBounce);
            LeanTween.scale(powerImage.gameObject, Vector3.one * 1.1f, 0.1f).setRepeat(7).setEase(LeanTweenType.pingPong);
            playerController.Rb.transform.localScale = Vector3.zero;

            Portal();
            Invoke(nameof(PlayerRb), 1.15f);

            LeanTween.scale(playerController.Rb.gameObject, Vector3.one, 0.5f).setEase( LeanTweenType.easeOutQuint );
        }

        public void Portal()
        {
            startPortal.SetActive(true);
            startPortal.transform.position = playerController.transform.position;
            startPortal.transform.rotation = playerController.transform.rotation;
            portalAnimation.Play("StartTutorial"); 
        }
        public void PlayerRb()
        {
            playerController.Rb.bodyType = RigidbodyType2D.Dynamic;
            //smokeEffect.SetActive(true);
            //fireEffect.SetActive(true);
        }
        public void CountdownStarts()
        {
            
        }
        private void SpawnEffect()
        {
            ParticleSystem ps = Instantiate(spawnEffect, playerController.Rb.position, Quaternion.identity);
        }

        private void Rotating(float val)
        {
            if(InputHandler.Instance.Thrust > 0)
            {
                LeanTween.cancel(rotateId);
                LeanTween.cancel(moveId);
                LeanTween.cancel(powerId);
                LeanTween.cancel(colorId);
                playerController.Unpause();

                
                playerController.Rb.AddForce(playerController.Rb.transform.up * power, ForceMode2D.Impulse);
                powerImage.enabled = false;

               
            }
        }

        public static void RegisterEntrance(StartSpot exit)
        {
            currentLevelEntraceList.Add(exit);
        }

        public static void ResetEnctranceList()
        {
            currentLevelEntraceList.Clear();
        }
    } 
}
