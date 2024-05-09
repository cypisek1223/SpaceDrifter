using SpaceDrifter2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SplineFillAnim : MonoBehaviour
{
    [Range(0,1)]
    public float fill = 1;
    public SpriteShapeController ssc;
    public float vert_count_mult = 0.33f;
    SpriteShapeFill ssfa;

    private void OnValidate()
    {
        if (!ssfa)
            ssfa = ScriptableObject.CreateInstance(typeof(SpriteShapeFill)) as SpriteShapeFill;

        ssc.modifiers.Clear();
        ssc.modifiers.Add(ssfa);

        ssfa.t01 = fill;
        ssfa.vert_count_mult = vert_count_mult;
        ssc.RefreshSpriteShape();
    }
}
