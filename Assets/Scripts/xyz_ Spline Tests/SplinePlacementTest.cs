using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace SpaceDrifter2D
{
    public class SplinePlacementTest : MonoBehaviour
    {
        [Range(0, 1)]
        public float progress = 0;

        public SpriteShapeController ssc;
        public bool openEnded;

        private void OnValidate()
        {
            Vector2 p = ssc.spline.GetPoint(progress, openEnded);
            transform.position = p;// ssc.transform.TransformPoint(p);
        }
    } 
}
