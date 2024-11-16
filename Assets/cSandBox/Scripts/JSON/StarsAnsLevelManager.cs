using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class StarsAnsLevelManager : MonoBehaviour
    {

        [SerializeField] private PlanetData planets;
        [SerializeField] private Canvas LevelUI;

        [SerializeField] private LevelButtons[] levelButtons;
        [SerializeField] private LevelStars[] levelStars;
        [SerializeField] private ScoreKeeper scoreKeeper;
        public DataLevel gameDate;

        #region StarsManager
        [System.Serializable]
        public class LevelStars
        {
            public Image star1;
            public Image star2;
            public Image star3;

            public void DowlandStarts(int level, int stars)
            {
                star1.enabled = stars >= 1;
                star2.enabled = stars >= 2;
                star3.enabled = stars >= 3;
                //Debug.Log("Level " + level + " :" + stars);
            }
        }

        [System.Serializable]
        public class LevelButtons
        {
            public Button button;
            public GameObject stars;
            public GameObject padlock;
            public Image Element1;
            public Image Element2;
            public Animator anim;
        }


        private void Start()
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();

            //STARS MANAGER
            for (int i = 0; i < levelButtons.Length; i++)
            {
                int levelIndex = i;

                if (gameDate.levels[i] == null)
                {
                    Debug.Log($"Element gameDate.levels[{i}] jest null. SprawdŸ, czy wszystkie poziomy s¹ zainicjalizowane.");
                    continue;
                }
                if (gameDate == null || gameDate.levels == null)
                {
                    Debug.Log("gameDate lub gameDate.levels jest null. Upewnij siê, ¿e zosta³y prawid³owo przypisane w Inspectorze.");
                    return;
                }


                levelButtons[levelIndex].button.onClick.AddListener(() => OnLevelSelected(gameDate.levels[levelIndex].level, planets));

                UpdateStarDisplay(levelIndex);
                LevelLockingSystem(levelIndex);

                levelButtons[i].anim.SetBool("Recomended", false);

                if (gameDate.levels[levelIndex].finished)
                {
                    Finished(i);
                }
                else if (gameDate.levels[levelIndex].recommended)
                {

                    RecommendLevel(levelIndex);
                }
                else
                {
                    continue;
                }

                    

               
                    
            }

            string sceneKey = $"{SceneManager.GetActiveScene().name}_FirstTime";
            if (PlayerPrefs.GetInt(sceneKey, 0) != 0)
            {
                // OPEN MENU LEVEL

            }
        }
        private void FinishedLevel(int _i)
        {

        }
        private void UpdateLockLevel(int _i,bool _active)
        {
            levelButtons[_i].button.interactable = _active;
            levelButtons[_i].stars.SetActive(_active);
            levelButtons[_i].padlock.SetActive(!_active);
        }
        private void UpdateStarDisplay(int _i)
        {
            if (levelStars[_i] == null)
            {
                Debug.Log($"levelStars[{_i}] jest null.");
                return;
            }

            if (gameDate.levels[_i] == null)
            {
                Debug.Log($"gameDate.levels[{_i}] jest null.");
                return;
            }

            int stars = gameDate.levels[_i].starts;
            levelStars[_i].DowlandStarts(_i, stars);
        }
        private void LevelLockingSystem(int _i)
        {
            if (gameDate.levels[_i].blocked)
            {
                UpdateLockLevel(_i, false);
            }
            else
            {
                UpdateLockLevel(_i, true);
            }
        }

        private void RecommendLevel(int _i)
        {
            levelButtons[_i].Element1.color = Color.blue;
            levelButtons[_i].Element2.color = Color.blue;

            //levelButtons[_i].anim.Play("Recomended");
            levelButtons[_i].anim.SetBool("Recomended", true);
        }

        private void Finished(int _i)
        {
            levelButtons[_i].Element1.color = Color.green;
            levelButtons[_i].Element2.color = Color.green;
        }
        #endregion

        #region LevelManager


        public void OnLevelSelected(LevelData level, PlanetData planetData)
        {
            GameManager.LevelLoad(level, planetData);
            //LevelUI.enabled = false;
        }
        #endregion
    }
}
