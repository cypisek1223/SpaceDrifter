using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class CameraController : Singleton<CameraController>
    {

        [SerializeField] private PlayerController player;
        [SerializeField] private CameraOrthoSizer cameraSizer;
        [SerializeField] private CameraBoundsController boundsSetter;
        [SerializeField] private BackgroundSetter backgroundSetter;
       //public CameraBeforPlayer cameraBeforPlayer;



        [SerializeField] CinemachineVirtualCamera gameplayVcam;

        [SerializeField] Logger logger;

        public float speed01 = 0.5f;
        private void Update()
        {
            float speed01 = player.NormalizedSpeed;
            logger.Log("Speed 01: " + speed01);
            cameraSizer.SizeCamera(gameplayVcam, speed01);
        }

        public void SetCameraCameraBlend(float speed01)
        {
            cameraSizer.SizeCamera(gameplayVcam, speed01);
        }

        public static void SetActive(bool active)
        {
            Instance.gameObject.SetActive(active);
        }

        public void SetInitialPosition(Vector2 position)
        {
            Vector3 pos = new Vector3(position.x, position.y, -10);
            gameplayVcam.transform.position = pos;
        }

        public void SetCameraBounds(Collider2D bounds)
        {
            boundsSetter.SetCameraBounds(gameplayVcam, bounds);
        }

        public void SetBackgroundMaterialAndColor(Material mat, Color primCol, Color secndCol)
        {
            //backgroundSetter.SetBackgroundMaterialAndColor(mat, primCol, secndCol);
        }
    }
}
