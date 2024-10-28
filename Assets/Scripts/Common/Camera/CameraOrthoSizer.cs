using Cinemachine;
using System;
using UnityEngine;

namespace SpaceDrifter2D
{
    internal class CameraOrthoSizer : MonoBehaviour
    {
        [SerializeField] Vector2 minMaxOrhtoSize = new( 7, 19 );

        //https://www.youtube.com/watch?v=KPoeNZZ6H4s
        private float prevSpeedInput;
        private float y, yd;
        [SerializeField] float startX = 0;
        [SerializeField] float f, z, r; // Steering parameters
        private float k1, k2, k3;

 

        private Vector3 offset;

        private void Start()
        {
            k1 = z / (Mathf.PI * f);
            k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
            k3 = r * z / (2 * Mathf.PI * f);

            prevSpeedInput = startX;
            y = startX;
            yd = 0;

        }
        
        private float SecondOrderDynamics(float newX, float deltaX)
        {
            y += Time.deltaTime * yd;
            yd = yd + Time.deltaTime * (newX + k3 * deltaX - y - k1 * yd) / k2;
            return y;
        }

       public void SizeCamera(CinemachineVirtualCamera gameplayVcam, float speed01)
       {
           speed01 = Mathf.Clamp(speed01, 0, 1);   // never trust noone xD
           var lensSettings = gameplayVcam.m_Lens;
           lensSettings.OrthographicSize = SmoothLerp(minMaxOrhtoSize.x, minMaxOrhtoSize.y, speed01);
           gameplayVcam.m_Lens = lensSettings;
       }

        public float smoothTime_accel = 1;
        public float smoothTime_slowDown = 1;
        float camPos;
        float camVel;

        private float SmoothLerp( float min, float max, float speedInput )
        {
            float smoothTime = speedInput > camPos ? smoothTime_accel : smoothTime_slowDown;
            camPos = Mathf.SmoothDamp(camPos, speedInput, ref camVel, smoothTime);
            return Mathf.Lerp(min, max, camPos);

            //float dSpeed = (speedInput - prevSpeedInput) / Time.deltaTime;
            //prevSpeedInput = speedInput;
            //return SecondOrderDynamics(speedInput, dSpeed);
        }
        
    }
}