using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseAnimated : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool open;

    private void Start()
    {
        animator.SetBool("open", open);
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
