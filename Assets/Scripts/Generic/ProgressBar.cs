using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float speed = 10;
    public float Progress01 { get; private set; }

    [SerializeField] private Image image;

    private void Start()
    {
        SetFill(0);
    }

    public void SetFill(float progress)
    {
        Progress01 = progress;
        //Progress01 = Mathf.Clamp(Progress01, 0, 1);
        Progress01 = Progress01 % 1;
        image.fillAmount = Progress01;
    }
}
