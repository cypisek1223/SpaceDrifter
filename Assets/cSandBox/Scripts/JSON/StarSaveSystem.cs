using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class StarSaveSystem : MonoBehaviour
    {
        #region Stars System

        [SerializeField] GameData gameDate;
        [SerializeField]  List<LevelDate2> previewBoard;
        [SerializeField] int levelStars;
        private void Start()
        {
            for (int i=0;i< gameDate.levels.Count; i++)
            {
                previewBoard[i].levelId = gameDate.levels[i].levelId;
                previewBoard[i].blocked = gameDate.levels[i].blocked;
                previewBoard[i].starts = gameDate.levels[i].starts;
                previewBoard[i].theBestTime = gameDate.levels[i].theBestTime;
                previewBoard[i].bestTime = gameDate.levels[i].bestTime;
                previewBoard[i].mediumTime = gameDate.levels[i].mediumTime;
                previewBoard[i].lowTime = gameDate.levels[i].lowTime;
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
        private int CalculateStars(GameData _gameDate, int levelId, float completionTime)
        {
            if (completionTime < _gameDate.levels[levelId].bestTime)
            {
                return 3;
            }
            else if (completionTime <= _gameDate.levels[levelId].mediumTime)
            {
                return 2;
            }
            else if(completionTime <= _gameDate.levels[levelId].lowTime)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int GetStars(int levelId, GameData gameDate)
        {
            return gameDate.levels[levelId].starts; ;
            
        }

        #endregion
    }
}
