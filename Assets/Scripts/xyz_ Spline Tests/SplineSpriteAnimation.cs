using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SplineSpriteAnimation : MonoBehaviour
{
    public int sprites = 2;
    SpriteShapeController controller;
    Spline spline;

    private void Start()
    {
        controller = GetComponent<SpriteShapeController>();
        spline = controller.spline;

        StartCoroutine(SetRandomly(0.1f));
    }
    IEnumerator SetRandomly(float freq)
    {
        while (true)
        {
            yield return new WaitForSeconds(freq);
            for (int i = 0; i < spline.GetPointCount(); i++)
            {
                spline.SetSpriteIndex(i, Random.Range(0, sprites));
            }
        }
    }
}
