using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D.MathFun
{
    public class UnityEventSwitch : OnOffSwitch
    {
        [SerializeField] private UnityEvent<bool> OnStateChanged;

        protected override void OnStateSet(bool active)
        {
            base.OnStateSet(active);
            OnStateChanged.Invoke(on);
        }
    }
}