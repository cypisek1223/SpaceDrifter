using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class CameraBounds : MonoBehaviour
    {
        [SerializeField] private Collider2D bounds;

        private void Start()
        {
            CameraController.Instance.SetCameraBounds(bounds);
        }
    }
}
