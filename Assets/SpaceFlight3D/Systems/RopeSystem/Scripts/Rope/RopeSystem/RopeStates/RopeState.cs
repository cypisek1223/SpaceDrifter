using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeMechanim2D
{
    public abstract class RopeState
    {
        protected RopeMechanim ropeMechanim;

        public RopeState(RopeMechanim ropeMechanim)
        {
            this.ropeMechanim = ropeMechanim;
        }

        public virtual void Toggle() { }

        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }
}