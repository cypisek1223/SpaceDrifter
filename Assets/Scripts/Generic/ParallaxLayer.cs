using UnityEngine;

namespace SpaceDrifter2D
{
    /// <summary>
    /// Used to move a transform relative to the main camera position with a scale factor applied.
    /// This is used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    [DefaultExecutionOrder(500)] // Needed to add this, since Cinemachine's LateUpdate could go first and parallax movement was jaggy
    public class ParallaxLayer : MonoBehaviour
    {
        /// <summary>
        /// Movement of the layer is scaled by this value.
        /// </summary>
        public Vector3 movementScale = Vector3.one;

        Transform followedObj;

        public Vector3 initialPosition;

        protected Vector3 followedOrigin;

        protected virtual void SetRelativePosition()
        {
            followedOrigin = followedObj.position;
        }

        protected virtual void Awake()
        {
            followedObj = Camera.main.transform;
            //initialPosition = transform.position;
            SetRelativePosition();
        }

        void LateUpdate()
        {
            if (!followedObj) return;

            transform.position = initialPosition + Vector3.Scale(followedObj.position - followedOrigin, movementScale);
           
        }

    }
}