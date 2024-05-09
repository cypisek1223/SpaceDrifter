using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class MeshLayer : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField] int layerIdx;
        [SerializeField] int sortOrder;
       // private SortingLayer layer;
        private Renderer meshRenderer;

        public void SetSortLayer(int selected)
        {
           // layer = SortingLayer.layers[selected];
            layerIdx = selected;
        }

        private void Start()
        {
            Set();
        }
        private void OnValidate()
        {
            Set();
        }

        private void Set()
        {
            meshRenderer = GetComponent<Renderer>();
            meshRenderer.sortingLayerID = SortingLayer.layers[layerIdx].id;
            meshRenderer.sortingOrder = sortOrder;
        }
    } 
}
