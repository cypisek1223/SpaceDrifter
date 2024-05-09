using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDrifter2D
{
    public class LevelListPanel : MonoBehaviour
    {
        public Action ListClosed;

        [SerializeField] private LevelInfoPanel prefab;
        [SerializeField] private Transform listContentParent;

        [SerializeField] SpaceDrifter2D.Logger logger;

        private LevelData selectedLevel;
        private Animator animator;

        private readonly int slideOutHash = Animator.StringToHash("slideOut");
        private bool transition;
        private bool initialised;

        private void Start()
        {
            animator = GetComponent<Animator>();

            //Quick fix. Start gets called on first open if GO was inactive earlier. So in case opening level list triggers Start for the first time, we ignore Closing it
            if (!initialised)
            {
                Close();
                initialised = true;
            }
        }

        private void Update()
        {
            if (transition) return;

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Input.mousePosition;
                RectTransform t = transform as RectTransform;

                //Detect clicking outside the list
                if(!RectTransformUtility.RectangleContainsScreenPoint( t, mousePos))
                {
                    transition = true;
                    animator.SetTrigger(slideOutHash);
                    Invoke(nameof(Close), 1);
                }
            }
        }
        private void Close()
        {
            transition = false;
            ListClosed?.Invoke();
            gameObject.SetActive(false);
        }

        public void DisplayLevels(List<LevelData> levels)
        {
            if (transition) return;
            initialised = true;

            gameObject.SetActive(true);

            foreach( Transform t in listContentParent)
            {
                if(t != listContentParent)
                {
                    Destroy(t.gameObject);
                }
            }
            foreach (var data in levels)
            {
                LevelInfoPanel levelPanel = Instantiate(prefab, listContentParent);
                levelPanel.Init(data, this);
            }

            FocusSelectedLevel();
        }

        private void FocusSelectedLevel()
        {
            Vector3 pos = listContentParent.position;
            pos.x = 0; //Currently just selecting first one
            listContentParent.position = pos;
        }

        public void SelectLevel(LevelData level)
        {
            logger.Log($"Klikniêto level: {level.Name}");
           // playButton.gameObject.SetActive(true);
            selectedLevel = level;

            Play();
        }
        
        public void Play()
        {
            GameManager.LoadLevel(selectedLevel);
        }
    } 
}
