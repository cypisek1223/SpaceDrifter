using UnityEngine;

namespace RopeMechanim2D
{
    public class RopeStateMachine : MonoBehaviour
    {

        protected RopeState state;

        public void ChangeState(RopeState newState)
        {
            if(state != null)
            {
                state.OnExit();
            }
            newState.OnEnter();

            state = newState;
        }

        //private RopeMechanim ropeMechanim;
        //public RopeMechanim RopeMechanim { get { return ropeMechanim; } }

        //public RopeStateMachine(RopeMechanim ropeMechanim)
        //{
        //    this.ropeMechanim = ropeMechanim;
        //    ChangeState(new RolledUpState(this));
        //}

        //public void Update()
        //{
        //    state.Update();
        //}
    }
}