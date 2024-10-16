using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class ShipAnimator : MonoBehaviour
    {
        Animator animator;
        float dmgTimer;

        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("dmg", 0);
        }

        public void AnimateDamage(float dmg, bool cirt)
        {
            //dmgTimer = 0;
            
                animator.SetFloat("dmg", dmg);
                //Invoke(nameof(StopDamage), 5.5f);
                Invoke(nameof(StopDamage), 5f);
           
        }

        void StopDamage()
        {
            animator.SetFloat("dmg", 0);
        }
    }
}
