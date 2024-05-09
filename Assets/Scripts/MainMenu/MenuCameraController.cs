using Cinemachine;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class MenuCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera vcam;
        [SerializeField] private Transform startTarget;

        private void Start()
        {
           // vcam.Follow = startTarget;
        }

        public void SetTarget(Transform target)
        {
            vcam.Follow = target;
        }
    } 
}
