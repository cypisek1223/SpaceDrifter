using UnityEngine;

namespace SpaceDrifter2D.MathFun
{
    public class SwitchBar : MonoBehaviour
    {
        int count;
        OnOffSwitch[] switches;
        float fill;

        private void Awake()
        {
            count = transform.childCount;
            switches = new OnOffSwitch[count];
            for (int i = 0; i < switches.Length; i++)
            {
                 switches[i] = transform.GetChild(i).GetComponent<OnOffSwitch>();
            }
        }

        /// <summary>
        /// Set bar fill amount in 0-1 range. Bar is made out of onOffSwitches, so it is grounded down to int.
        /// </summary>
        /// <param name="v">Fill amount in 0-1 range</param>
        public void SetVolume(float v)
        {
            int fillIntAmount = Mathf.RoundToInt(switches.Length * Mathf.Clamp(v, 0, 1));
            for(int i=0; i < fillIntAmount; i++)
            {
                switches[i].Set(true);
            }

            for(int i= fillIntAmount; i < switches.Length; i++)
            {
                switches[i].Set(false);
            }
        }

        private void OnStart(OnOffSwitch @switch)
        {
            @switch.Switch();
        }
    }
}