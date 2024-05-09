using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class Screw : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] bool open;

        private void Start()
        {
            animator.SetBool("open", open);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            open = true;
            animator.SetBool("open", open);
        }

        public void Close()
        {
            open = false;
            animator.SetBool("open", open);
        }
    }
}
