using System;
using UnityEngine;
using Cinemachine;

namespace SpaceDrifter2D
{
    public class CameraBoundsController : Singleton<CameraBoundsController>
    {
        public void SetCameraBounds(CinemachineVirtualCamera gameplayVcam, Collider2D bounds)
        {
            CinemachineConfiner2D confiner = gameplayVcam.GetComponent<CinemachineConfiner2D>();
            confiner.m_BoundingShape2D = bounds;
        }
    }
}
