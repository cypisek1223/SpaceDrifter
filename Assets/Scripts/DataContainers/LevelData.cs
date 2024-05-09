using UnityEngine;
using NaughtyAttributes;
using System;

namespace SpaceDrifter2D
{
    [Serializable]
    public class LevelData
    {
        public string Name => name;
        public string Scene => scene;
        public LevelType Type => levelType;
        public bool Fade => fade;
        public PlanetData Planet { get; private set; }

        public int Seed => seed;
        public int Length => length;

        //Player Score Info
        public int CollectedCoins { get; set; }
        public int FinishTime { get; set; }
        public int CollectedStars { get; set; }

        //Availability
        public bool Locked => locked;

        //Game Management info
        [SerializeField] private string name;
        [Scene][SerializeField] private string scene;
        [SerializeField] private LevelType levelType = LevelType.Arcade;
        [SerializeField] private bool fade = true;
        [SerializeField] private bool locked = true;

        [EnableIf("levelType", LevelType.Generated)]
        [SerializeField] int seed = 1;
        [EnableIf("levelType", LevelType.Generated)]
        [SerializeField] int length = 10;

        public void SetPlanet(PlanetData planet)
        {
            Planet = planet;
        }

        public void Unlock()
        {
            locked = false;
        }
    }

    public enum LevelType
    {
        MainMenu,
        Arcade,
        Generated
    }
}