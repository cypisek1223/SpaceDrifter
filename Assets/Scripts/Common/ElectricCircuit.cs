using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class ElectricCircuit : MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void PowerOn()
        {
            animator.SetTrigger("power");
        }
    }
}
