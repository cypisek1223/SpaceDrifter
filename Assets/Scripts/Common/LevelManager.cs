using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SpaceDrifter2D
{
    public class LevelManager : Singleton<LevelManager>
    {
        public int PackagesToDeliver => packageSlots.Count;

        private EntrancePicker exitPicker;
        private List<FinishSpot> exitList = new List<FinishSpot>();
        private List<LevelTilemap> maps = new List<LevelTilemap>();
        private List<PackageSlot> packageSlots = new List<PackageSlot>();

        private FinishSpot activeExit;

        protected override void Awake()
        {
            base.Awake();
            exitPicker = ScriptableObject.CreateInstance<EntrancePicker>();
        }

        public void ResetLevel() //Must be invoked before level is loaded, bcuz new splashes register then
        {
            maps.Clear();
            exitList.Clear();
            packageSlots.Clear();
        }

        internal void Register(PackageSlot packageSlot)
        {
            packageSlots.Add(packageSlot);
        }

        public void Register(FinishSpot exit)
        {
            exitList.Add(exit);
        }

        public void Register(LevelTilemap map)
        {
            maps.Add(map);
        }

        public void PaintMaps(Color color)
        {
            foreach(var m in maps)
            {
                m.Tilemap.material.color = color;
            }
        }

        public FinishSpot GetExit()
        {
            if(activeExit == null)
            {
                activeExit = exitPicker.PickExit(exitList);
            }
            return activeExit;
        }

        internal PackageSlot GetPackageSlot()
        {
            return packageSlots[0];
        }

        public bool OpenExit()
        {
            if(PackagesToDeliver == 0)
            {
                FinishSpot exit = GetExit();
                exit.Open();
                return true;
            }
            return false;
        }

        internal void PackageDelivered(PackageSlot slot)
        {
            FinishSpot exit = GetExit();
            ArrowNavigation.Instance.SetTarget(exit.transform);
            packageSlots.Remove(slot);
            OpenExit();
        }
    } 
}
