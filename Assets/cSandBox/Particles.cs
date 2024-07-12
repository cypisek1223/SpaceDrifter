using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Transform playerTransform;

    void Update()
    {


        ///Vector3 currentRotation = transform.eulerAngles;
        ///currentRotation.z = playerTransform.eulerAngles.z;
        ///transform.eulerAngles = currentRotation;

        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = playerTransform.eulerAngles.z;
        transform.eulerAngles = currentRotation;

        if (!particleSystem.isPlaying)
        {
            // Uruchom system cz¹steczek
            particleSystem.Play();
        }

    }
}
