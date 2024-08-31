using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class FinishPortal : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] Transform targetPosition;
        
        [SerializeField] Animator portalAnim;


        //DO ZMIANY TO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public GameObject scoreKeeperObject;
        public GameObject finishCanvas;
        public ScoreKeeper sc;
        //[SerializeField] UnityEvent OnOpen;
        //[SerializeField] UnityEvent OnClose;


        [SerializeField] bool open = true;
        
        private void Start()
        {
            //Close();
            portalAnim.Play("StartingLeveling");
            
            finishCanvas = GameObject.FindGameObjectWithTag("FinishLevelPanel");
            scoreKeeperObject = GameObject.FindGameObjectWithTag("ScoreKeeper");
            sc = scoreKeeperObject.GetComponent<ScoreKeeper>();

            //LevelManager.Instance?.Register(this);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && open)
            {
                Debug.Log("Player IN Portal");
                PlayerController pcont = collision.gameObject.GetComponentInParent<PlayerController>();
                pcont.TurnEnginesOff();
                LeanTween.move(collision.gameObject, targetPosition.position, 3f).setEase(LeanTweenType.easeOutQuint);
                //LeanTween.scale(collision.gameObject, Vector3.zero, 1).setEase(LeanTweenType.easeOutQuint).setOnComplete(FinishLevel);
                portalAnim.Play("EndingLevels");
            }
        }
        
        private void FinishLevel()
        {
            Debug.Log("FINISH LEVEL");
            GameManager.FinishLevel();
            sc.FinishCurrentLevel();
            finishCanvas.SetActive(true);
        }
        
        public virtual void Close()
        {
            open = false;
            //OnClose.Invoke();
        }
        
        public virtual void Open()
        {
            open = true;
            //this.gameObject.SetActive(true);
            //OnOpen.Invoke();
        }
    }
}