using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D.MathFun
{
    public class OnOffSwitch : MonoBehaviour
    {
        protected bool on;

        private void Awake()
        {
            Init();
        }

        public void Set(bool active)
        {
            on = active;
            OnStateSet(on);
        }

        public void Switch()
        {
            Set(!on);
        }

        protected virtual void Init() { }
        protected virtual void OnStateSet(bool active) { }
    } 
}
