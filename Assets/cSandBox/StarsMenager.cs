using UnityEngine.UI;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class StarsMenager : MonoBehaviour
    {
        [System.Serializable]
        public class LevelStars
        {
            public Image star1;
            public Image star2;
            public Image star3;

            public void UpdateStarsDisplay(int level, int stars)
            {
                star1.enabled = stars >= 1;
                star2.enabled = stars >= 2;
                star3.enabled = stars >= 3;
                //Debug.Log("Level " + level + " :" + stars);
            }
        }

        [SerializeField] private Button[] levelButtons;
        [SerializeField] private LevelStars[] levelStars;
        [SerializeField] private ScoreKeeper scoreKeeper;

        private void Start()
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();

            for (int i = 0; i < levelButtons.Length; i++)
            {
                int levelIndex = i;
                UpdateStarDisplay(levelIndex);
            }
        }

        private void UpdateStarDisplay(int levelIndex)
        {
            int stars = scoreKeeper.GetStars(levelIndex);
            levelStars[levelIndex].UpdateStarsDisplay(levelIndex, stars);


        }
    }
}
