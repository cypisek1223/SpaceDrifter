using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NaughtyAttributes;

namespace SpaceDrifter2D
{
    public class TextMeshToParticleMaterial : MonoBehaviour
    {
        public TextMeshPro textMeshPro;
        public ParticleSystem textParticleSystem;
        private ParticleSystemRenderer rendererSystem;
        // Start is called before the first frame update
        void Start()
        {
            rendererSystem = textParticleSystem.GetComponent<ParticleSystemRenderer>();
            rendererSystem.mesh = textMeshPro.mesh;
            rendererSystem.material = textMeshPro.fontMaterial;
        }

        internal void SetText(string text)
        {
            textMeshPro.text = text;
            Init();
        }

        [Button]
        public void Init()
        {
            Start();
        }
    } 
}
