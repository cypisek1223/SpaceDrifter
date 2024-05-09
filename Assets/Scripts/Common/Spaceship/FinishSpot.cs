using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class FinishSpot : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] Transform targetPosition;

        [SerializeField] UnityEvent OnOpen;
        [SerializeField] UnityEvent OnClose;

        private bool open;

        private void Start()
        {
            Close();
            LevelManager.Instance?.Register(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && open)
            {
                PlayerController pcont = collision.gameObject.GetComponentInParent<PlayerController>();
                pcont.TurnEnginesOff();
                LeanTween.move(collision.gameObject, targetPosition.position, 1).setEase(LeanTweenType.easeOutQuint);
                LeanTween.scale(collision.gameObject, Vector3.zero, 1).setEase(LeanTweenType.easeOutQuint).setOnComplete(FinishLevel);
            }
        }

        private void FinishLevel()
        {
            GameManager.FinishLevel();
        }

        public virtual void Close()
        {
            open = false;
            OnClose.Invoke();
        }

        public virtual void Open()
        {
            open = true;
            OnOpen.Invoke();
        }
    } 
}
