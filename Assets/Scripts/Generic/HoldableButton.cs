using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceDrifter2D
{
    public class HoldableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsHeld { get; private set; }

        public virtual void OnPointerDown(PointerEventData ped)
        {
            IsHeld = true;
        }

        public virtual void OnPointerUp(PointerEventData ped)
        {
            IsHeld = false;
        }
    }
}