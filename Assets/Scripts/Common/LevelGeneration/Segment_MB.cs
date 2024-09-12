using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class Segment_MB : MonoBehaviour
    {
        public PathGenerator ParentGenerator { get; set; }

        public Vector3 Entrance => entrance.position;
        public Vector3 Exit => exit.position;

        public EntranceType TypeIn => typeIn;
        public EntranceType TypeOut => typeOut;

        public DirectionChange DirectionChange => directionChange;

        public Segment_MB Previous { get; set; }

        [SerializeField] bool isLast;

        [SerializeField] private Transform entrance;
        [SerializeField] private Transform exit;
        [SerializeField] private EntranceType typeIn;
        [SerializeField] private EntranceType typeOut;
        [SerializeField] private DirectionChange directionChange;

        [SerializeField] private UnityEvent onPlayerEnter;

        private bool closedAlready;

        public void PlaceByEntranceAt(Vector3 position)
        {
            transform.position = position;
            transform.position -= entrance.rotation * entrance.localPosition;
           
            //Debug.Log("Pozycja to: " + transform.position +" entren position "+ entrance.localPosition +" position :"+ position);
        }

        public void SetRotation(Quaternion prev_rot, DirectionChange directionChange)
        {
            Quaternion new_rot = prev_rot * Quaternion.AngleAxis((int)directionChange, Vector3.forward);
            transform.rotation = new_rot;
        }

        //Consider moving it to entrance script
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                if(!closedAlready)
                {
                    closedAlready = true;
                    PlayerEntered();
                }
            }
        }

        void PlayerEntered()
        {
            //Debug.Log("Player entered seg: " + name);
            onPlayerEnter.Invoke();
            if (isLast)
            {
                ParentGenerator.EndReached();
                return;
            }

            ParentGenerator.MovePlayer();
        }
    }
}