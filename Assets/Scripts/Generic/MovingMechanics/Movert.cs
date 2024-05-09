using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Movert : MonoBehaviour
    {
        public Transform handle, start, end;
        public float time = 4;
        public LeanTweenType easeType;
        public bool startAtEnd;
        public bool moveAtStart = true;
        public bool autoComeback;
        public float comebackCooldown;

        private int leanId;
        private bool toEnd;
        private bool pending;

        private void Start()
        {
            Vector2 startPos = !startAtEnd ? start.position : end.position;
            handle.position = startPos;
            toEnd = !startAtEnd;
            if (moveAtStart)
            {
                Move(toEnd);
            }
        }

        private void OnDestroy()
        {
            ClearTween();
        }

        public void Stop()
        {
            ClearTween();
        }

        public void Move()
        {
            if (pending)
                toEnd = !toEnd;
            Move(toEnd);
        }

        void OnArive()
        {
            pending = false;
            toEnd = !toEnd;
            if(autoComeback)
            {
                Invoke(nameof(Move), comebackCooldown);
            }
        }

        void Move(bool toEnd)
        {
            pending = true;
            ClearTween();
            Vector2 targetPos = toEnd ? end.position : start.position;
            leanId = LeanTween.move(handle.gameObject, targetPos, time).setEase(easeType).setOnComplete(OnArive).id;
        }


        void ClearTween()
        {
            if (leanId != -1)
                LeanTween.cancel(leanId);
        }
    } 
}
