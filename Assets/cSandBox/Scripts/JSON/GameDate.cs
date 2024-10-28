using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "DataLevel", menuName = "GameManagement/DataLevel")]
    public class GameData: ScriptableObject
    {
        public List<LevelDate2> levels;
    }
}