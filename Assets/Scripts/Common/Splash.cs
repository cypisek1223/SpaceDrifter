using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] new private Renderer renderer;

        private void Start()
        {
            SplashManager.Instance.Register(this);
        }

        public void SetMaterial(Material mat)
        {
            //renderer.sortingLayerName = "Foreground";
            renderer.material = mat;
        }
        public void Paint(Color primCol, Color secndCol)
        {
            renderer.material.SetColor("_PrimColor", primCol);
            renderer.material.SetColor("_SecndColor", secndCol);
        }
    } 
}
