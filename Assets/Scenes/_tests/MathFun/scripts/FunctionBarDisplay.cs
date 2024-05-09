using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D.MathFun
{
    public class FunctionBarDisplay : MonoBehaviour
    {
        [SerializeField]private Function1D f;
        [SerializeField]private float offset = 0.1f;
        [SerializeField]private float speed = 0.1f;

        private SwitchBar[] bars;
        int count;

        private void Start()
        {
            count = transform.childCount;
            bars = new SwitchBar[count];
            for (int i = 0; i < bars.Length; i++)
            {
                bars[i] = transform.GetChild(i).GetComponent<SwitchBar>();
            }
        }
        private void Update()
        {
            //float volume = f.Evaluate(Time.time * speed);
            //bars[0].SetVolume(volume);
            //bars[1].SetVolume(volume);
            for (int i = 0; i < bars.Length; i++)
            {
                float volume = f.Evaluate(Time.time * speed + i * offset);
                bars[i].SetVolume(volume);
            }
        }
    } 
}
