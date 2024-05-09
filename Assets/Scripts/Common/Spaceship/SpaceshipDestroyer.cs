using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SpaceshipDestroyer : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CollisionSystem collisionSystem;

        [SerializeField] private ParticleSystem destroyEffect;

        [SerializeField] private AudioClip explosionSfx;
        [SerializeField] private float explosionSfxVolume = 1;

        //private void Start()
        //{
        //    collisionSystem.OnTrigger += DestroyShip;
        //}

        //private void OnDestroy()
        //{
        //    collisionSystem.OnTrigger -= DestroyShip;
        //}

        public void DestroyShip(Vector2 position, Vector2 normal)
        {
            playerController.Kill();

            //Sound
            SoundManager.Instance.PlaySfx(explosionSfx, explosionSfxVolume);

            //Destroy effect
            destroyEffect.transform.position = position;
            destroyEffect.transform.up = normal;
            destroyEffect.Play();

            Invoke(nameof(KillPlayer), 1);
        }

        private void KillPlayer()
        {
            GameManager.KillPlayer();
        }
    }
}
