using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceDrifter2D
{
    public class HoldableDoublePressButton : HoldableButton
    {
        public Action OnDoublePress;
        public Action OnDoubleLeft;

        float timeSincePress;
        public bool DoublePressed { get; private set; }

        public override void OnPointerDown(PointerEventData ped)
        {
            base.OnPointerDown(ped);

            bool alreadyPressed = DoublePressed;
            DoublePressed = timeSincePress > 0;
            if (!alreadyPressed && DoublePressed)
            {
                OnDoublePress?.Invoke();
            }
        }

        public override void OnPointerUp(PointerEventData ped)
        {
            base.OnPointerUp(ped);
            if(DoublePressed)
            {
                OnDoubleLeft?.Invoke();
            }
            else
            {
                timeSincePress = 0.35f; //Time offset during which another Press is counted as double Press
            }
            DoublePressed = false;
        }

        private void Update()
        {
            timeSincePress -= Time.deltaTime;
        }
    }
}