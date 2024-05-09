using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class WaypointManager : MonoBehaviour
    {
        private Waypoint[] waypoints;
        private int current;

        private void Start()
        {
            int count = transform.GetComponentsInChildren<Waypoint>(false).Length; //Count for active children (exclude self)
            waypoints = new Waypoint[count]; // transform.childCount];

            current = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (!child.gameObject.activeSelf) continue; //discard inactive checkpoints

                waypoints[current] = child.GetComponent<Waypoint>();
                waypoints[current].Init( this, current );
                current++;
            }

            current = 0;
            ArrowNavigation.Instance?.SetTarget(waypoints[current].transform);
        }

        public void Progress()
        {
            if(current == waypoints.Length-1)
            {
                OnCompleted();
            }
            else
            {
                current++;
                ArrowNavigation.Instance.SetTarget(waypoints[current].transform);
            }
        }

        private void OnCompleted()
        {
            bool open = LevelManager.Instance.OpenExit();
            if(open)
            {
                FinishSpot exit = LevelManager.Instance?.GetExit();
                ArrowNavigation.Instance?.SetTarget(exit.transform);
            }
            else
            {
                PackageSlot slot = LevelManager.Instance.GetPackageSlot();
                ArrowNavigation.Instance?.SetTarget(slot.transform);
            }
        }

        public bool IsCurrent(Waypoint waypoint)
        {
            return Array.IndexOf(waypoints, waypoint) == current;
        }
    } 
}
