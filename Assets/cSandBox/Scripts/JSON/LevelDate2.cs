using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDrifter2D
{
    [System.Serializable]
    public class LevelDate2
    {
        public int levelId;
        public bool blocked;
        public int starts;
        public float theBestTime;
        public float bestTime;
        public float mediumTime;
        public float lowTime;

        public Image[] stars;
        public Button button;
    }
}
