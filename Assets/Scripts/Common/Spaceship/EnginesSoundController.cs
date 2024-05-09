using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class EnginesSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float thrustVolume = 1.1f;
        [SerializeField] private float thrustPitch = 1.1f;
        [SerializeField] private float steadyVolume = 0.85f;
        [SerializeField] private float steadyPitch = 0.9f;

        [SerializeField] private float volumeChangeSpeedUp = 1;
        [SerializeField] private float volumeChangeSpeedDown = 1;
        [SerializeField] private float pitchChangeSpeedUp = 1;
        [SerializeField] private float pitchChangeSpeedDown = 1;

        private void OnEnable()
        {
            audioSource.Play();
        }

        private void OnDisable()
        {
            audioSource.Stop();
        }

        private void Update()
        {
            if (InputHandler.Instance.Thrust > 0)
            {
                Thrust();
            }
            else
            {
                Steady();
            }
        }

        private void Thrust()
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, thrustVolume, Time.deltaTime * volumeChangeSpeedUp);
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, thrustPitch, Time.deltaTime * pitchChangeSpeedUp);
        }

        private void Steady()
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, steadyVolume, Time.deltaTime * volumeChangeSpeedDown);
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, steadyPitch, Time.deltaTime * pitchChangeSpeedDown);
        }
    }
}
