using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class ArrowNavigation : Singleton<ArrowNavigation>
    {
        [SerializeField] private GameObject arrow;
        private Transform currentTarget;

        private void Update()
        {
            if (currentTarget == null) return;
            transform.up = (currentTarget.position - transform.position).normalized;
        }

        public void SetTarget(Transform target)
        {
            currentTarget = target;
        }

        internal void HideArrow()
        {
            arrow.SetActive(false);
        }

        internal void ShowArrow()
        {
            arrow.SetActive(true);
        }
    } 
}
