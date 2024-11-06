using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceDrifter2D;

namespace SpaceDrifter2D
{
    [CreateAssetMenu(fileName = "DataLevel", menuName = "GameManagement/DataLevel")]
    public class DataLevel : ScriptableObject
    {
        public LevelDate2[] levels;
        public int predictetLevel;
    }
}
