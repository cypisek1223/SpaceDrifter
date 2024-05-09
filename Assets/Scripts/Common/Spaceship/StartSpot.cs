using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class StartSpot : MonoBehaviour
    {
        public Vector2 startPos => playerPos.position;
        public Quaternion startRot => playerPos.rotation;

        [SerializeField] private Transform playerPos;

        private void Start()
        {
            LevelSetuper.Instance?.SetPlayerStartPos(playerPos.position, playerPos.rotation);
            Respawner.RegisterEntrance(this);
        }
    } 
}
