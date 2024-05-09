using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SplineWave : MonoBehaviour
{
    [SerializeField] SpriteShapeController ss_controller;
    [SerializeField] Spline spline;
    public float waveScale = 1;
    private void Start()
    {
        ss_controller = GetComponent<SpriteShapeController>();
        spline = ss_controller.spline;
    }

    private void Update()
    {
        for(int i=0; i<spline.GetPointCount()-1;i++)
        {
            Vector3 pos = spline.GetPosition(i);
            Vector3 normal = (pos - transform.position).normalized;
            pos += normal * Mathf.Sin(Time.time + i) * waveScale;
            spline.SetPosition(i, pos);
        }
    }
}
