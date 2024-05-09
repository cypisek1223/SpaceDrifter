
using UnityEngine;

namespace RopeMechanim2D
{
    public class RolledUpState : RopeState
    {
        Transform hook => ropeMechanim.hook;

        public RolledUpState(RopeMechanim ropeMechanim) : base(ropeMechanim) { }

        public override void OnEnter()
        {
        }

        public override void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                ropeMechanim.ChangeState(new RollingDownState(ropeMechanim));
            }
        }

        public override void Toggle()
        {
            ropeMechanim.ChangeState(new RollingDownState(ropeMechanim));
        }

    }
}