using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] float textureSpeed = 1;
        [SerializeField] private PlayerController playerController;
        private Rigidbody2D playerRb;
        private Material mat;
        private float t;

        
        private void Start()
        {
            playerRb = playerController.Rb;
            mat = GetComponent<Renderer>().material;
            t = 0;
        }

        private void Update()
        {
            t += Time.deltaTime * textureSpeed;
            // Vector2 offsetSpeed = Vector2.Lerp( mat.GetVector("_PatternDistort"), playerRb.velocity * textureSpeed, Time.deltaTime * changeSpeed);
            //mat.SetVector("_PatternDistort", offsetSpeed);
            Vector2 distortion = new Vector2(Mathf.Sin(t), Mathf.Cos(t));
            mat.SetVector("_PatternDistort", distortion);
        }
    }
}