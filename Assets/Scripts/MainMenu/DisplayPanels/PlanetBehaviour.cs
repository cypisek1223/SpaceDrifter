using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceDrifter2D
{
    public class PlanetBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public PlanetData Data { get { return data; } }
        public Texture ArtTex { get { return bloomLensDirtTex; } }

        [SerializeField] private PlanetData data;

        [SerializeField] private GameObject lockedState;
        [SerializeField] private GameObject unlockedState;

        [SerializeField] private SpriteRenderer atmoSprite;
        [SerializeField] private Renderer firstLayer;
        [SerializeField] private Texture bloomLensDirtTex;

        [SerializeField] private Logger logger;

        private event Action<PlanetBehaviour> PlanetClicked;

        private void Start()
        {
            data.AssignPlanetLevels();

            //Color atmoCol = data.PrimaryColor;
            //atmoCol.a = atmoSprite.color.a;
            //atmoSprite.color = atmoCol;
            //firstLayer.material.SetColor("Color_5fe9752705a8428b9d8ba5bbc7573718", data.SecondaryColor);

            if(data.Locked)
            {
                lockedState.SetActive(true);
            }
            else
            {
                lockedState.SetActive(false);
            }
        }

        public void SetOnClick(Action<PlanetBehaviour> OnClick)
        {
            PlanetClicked += OnClick;
        }

        public void OnPointerUp(PointerEventData ped)
        {
            if (data.Locked) return;

            logger.Log($"Planet: {data.Name} clicked!");
            PlanetClicked?.Invoke(this);
       //     GameManager.Instance.SelectPlanet(planet);
        }

        public void OnPointerDown(PointerEventData ped)
        {

        }
    } 
}
