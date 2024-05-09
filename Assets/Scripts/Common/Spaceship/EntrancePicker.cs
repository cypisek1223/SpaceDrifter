using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class EntrancePicker : ScriptableObject
    {
        public StartSpot PickEntrance(List<StartSpot> entranceList)
        {
            return entranceList[0];
        }

        public FinishSpot PickExit(List<FinishSpot> exitList)
        {
            return exitList[Random.Range(0, exitList.Count)];
        }
    }
}