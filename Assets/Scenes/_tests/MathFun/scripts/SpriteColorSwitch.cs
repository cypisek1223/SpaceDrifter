using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D.MathFun
{
    public class SpriteColorSwitch : OnOffSwitch
    {
        [SerializeField]private SpriteRenderer sprite;
        [SerializeField]private Color activeColor;
        [SerializeField]private Color inactiveColor;

        //protected override void Init()
        //{
        //    sprite = GetComponent<SpriteRenderer>();
        //}

        protected override void OnStateSet(bool active)
        {
            sprite.color = active ? activeColor : inactiveColor;
        }
    } 
}
