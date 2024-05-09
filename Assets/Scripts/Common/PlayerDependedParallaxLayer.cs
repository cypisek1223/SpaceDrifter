using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    [ExecuteAlways]
    public class PlayerDependedParallaxLayer : ParallaxLayer
    {
        public Vector3 origin;

        protected override void SetRelativePosition()
        {
            followedOrigin = origin;
        }

        private void Update()
        {
            SetRelativePosition(); //This is not optimal and needs to be removed.
        }
        //protected override void Awake()
        //{
        //    LevelLoader.SceneRevealed += SetInitialPosition;
        //}

        //private void OnDestroy()
        //{
        //    LevelLoader.SceneRevealed -= SetInitialPosition;
        //}

        //private void SetInitialPosition(LevelData d)
        //{
        //    base.Awake();
        //}
    }
}
