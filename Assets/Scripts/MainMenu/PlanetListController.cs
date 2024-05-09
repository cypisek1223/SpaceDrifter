using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SpaceDrifter2D
{
    public class PlanetListController : MonoBehaviour
    {
        [SerializeField] private DataManager dataManager;
        [SerializeField] private PlanetBehaviour[] planets;
        [SerializeField] private MenuCameraController menuCamera;
        [SerializeField] private PlanetInfoPanel planetInfoDisplay;
        [SerializeField] private LevelListPanel levelsDisplay;
        [SerializeField] private Volume globalVolume;
        private int current;

        //Screen dragging
        bool fingerDown;
        Vector3 startPos;
        Vector3 delta;
        bool disabled;
        bool transition;


        private void OnDestroy()
        {
            levelsDisplay.ListClosed -= OnLevelListClose;
        }

        private void Start()
        {
            levelsDisplay.ListClosed += OnLevelListClose;
            foreach (var p in planets)
            {
                p.SetOnClick(OnPlanetClick);
            }

            current = Array.IndexOf(planets, Array.Find(planets, p => p.Data == dataManager.CurrentPlanet));
            if (current == -1)
                current = 0;
            FrameCurrentPlanet();
        }

        //Refactor this to be UI dragged element instead of general input drag detecting element
        private void Update()
        {
            if (disabled) return;

            if (Input.GetMouseButtonDown(0) && !fingerDown)
            {
                fingerDown = true;
                startPos = Input.mousePosition;
            }

            if (fingerDown && Input.GetMouseButtonUp(0))
            {
                fingerDown = false;
                if (Mathf.Abs(delta.x) > 100) //Ignore small swipe to prevent switching by clicking
                {
                    //Debug.Log($"Delta X: {delta.x}; Delta Y: {delta.y}");
                    bool left = delta.x < 0 ? false : true; //Sides are  switched bcuz draging left moves us right ;)
                    NextPlanet(left);
                }
                else
                {
                    transition = false;
                }
            }
            else if(fingerDown)
            {
                Vector3 endPos = Input.mousePosition;
                delta = endPos - startPos;
            }

            if(fingerDown && Mathf.Abs(delta.x) > 50)
            {
                transition = true;
            }
        }

        private void OnPlanetClick(PlanetBehaviour planet)
        {
            if (transition) return;

            GameManager.SetCurrentPlanet(planet.Data);

            //planetInfoDisplay.Deactivate();
            disabled = true;
            fingerDown = false;
            List<LevelData> levels = planet.Data.AllLevels;
            levelsDisplay.DisplayLevels(levels);
        }
        private void OnLevelListClose()
        {
            disabled = false;
        }

        private void NextPlanet(bool left)
        {
            current = left ? current - 1 : current + 1;
            current = Mathf.Clamp(current, 0, planets.Length);
            current = current % planets.Length;

            FrameCurrentPlanet();
            Invoke(nameof(FinishTransition), 1);
        }
        private void FinishTransition()
        {
            transition = false;
        }

        private void FrameCurrentPlanet()
        {
            Bloom bloom;
            if(globalVolume.profile.TryGet(out bloom))
                bloom.dirtTexture.value = planets[current].ArtTex;

            menuCamera.SetTarget(planets[current].transform);
            planetInfoDisplay.SetPlanet(planets[current].Data);
        }
    } 
}
