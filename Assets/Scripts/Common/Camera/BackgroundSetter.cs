using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class BackgroundSetter : MonoBehaviour
    {
        [SerializeField] private Renderer backgroundSpace;
        public void SetBackgroundMaterialAndColor(Material mat, Color primCol, Color secndCol)
        {
            backgroundSpace.material = mat;
            backgroundSpace.material.SetColor("_PrimColor", primCol);
            backgroundSpace.material.SetColor("_SecndColor", secndCol);
        }
    }
}
