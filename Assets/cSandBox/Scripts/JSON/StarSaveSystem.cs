using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class StarSaveSystem : MonoBehaviour
    {
        #region Stars System

        [SerializeField] DataLevel gameDate;
        [SerializeField]  List<LevelDate2> previewBoard;
        [SerializeField] int levelStars;
        private void Start()
        {
            for (int i=0;i< gameDate.levels.Length; i++)
            {
                //previewBoard[i].levelId = gameDate.levels[i].levelId;
                previewBoard[i].blocked = gameDate.levels[i].blocked;
                previewBoard[i].starts = gameDate.levels[i].starts;
                previewBoard[i].theBestTimeForStars = gameDate.levels[i].theBestTimeForStars;
                previewBoard[i].bestTime = gameDate.levels[i].bestTime;
                previewBoard[i].mediumTimeForStars = gameDate.levels[i].mediumTimeForStars;
                previewBoard[i].lowTimeForStars = gameDate.levels[i].lowTimeForStars;
            }
        }

      
        public void SaveLevelCompletionTime(int levelId, float completionTime)
        {


            var level = gameDate.levels[levelId];
            if (level != null)
            {
                Debug.Log("Zapisywanie Levela");
                levelStars = CalculateStars(gameDate, levelId, completionTime);


                if (levelStars > gameDate.levels[levelId].starts)
                {
                    gameDate.levels[levelId].starts = levelStars;
                }
                                
                if (gameDate.levels[levelId].bestTime < completionTime)
                    gameDate.levels[levelId].bestTime = completionTime;

            }

        }
        private int CalculateStars(DataLevel _gameDate, int levelId, float completionTime)
        {
            if (completionTime < _gameDate.levels[levelId].bestTime)
            {
                return 3;
            }
            else if (completionTime <= _gameDate.levels[levelId].mediumTimeForStars)
            {
                return 2;
            }
            else if(completionTime <= _gameDate.levels[levelId].lowTimeForStars)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int GetStars(int levelId, DataLevel gameDate)
        {
            return gameDate.levels[levelId].starts; ;
            
        }

        #endregion
    }
}
