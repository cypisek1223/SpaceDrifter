using SpaceDrifter2D;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

// A simple C# job to move the UV along the x-axis. If this is called repeatedly each frame it creates UV Animation effect. To get this called each frame, use RefreshSpriteShape API of SpriteShapeController.
public struct FillJob : IJob
{
    //public Vector2 zeroPosition;
    public NativeSlice<Vector3> positions;

    public float t01;       //0.5
    public float mult;

    public void Execute()
    {
        int pos_count = positions.Length;   //12
        pos_count = (int)Mathf.Ceil(pos_count * mult); // for whatever reason 2/3 of verts are useless
        int half_pos_count = pos_count / 2; //just assume it's even (not odd)   //6
        int quads_count = pos_count / 4;

        float fill = ((float)(quads_count)) * t01;  //2.5
        int index = ((int)fill) * 4;
        float further_offset = fill - ((int)fill);  //0.5

        CutFromBottom(index, further_offset);
    }

    void CutFromBottom_step(int last_untouched, float further_offset)
    {
        if (last_untouched < positions.Length - 2) //lerping
        {
            // segment by segment
            positions[last_untouched + 1] = positions[last_untouched - 1];
            positions[last_untouched + 2] = positions[last_untouched];
        }

        for (int i = last_untouched + 4; i < positions.Length; i += 4)
        {
            positions[i] = positions[last_untouched];
            positions[i + 1] = positions[last_untouched];
            positions[i + 2] = positions[last_untouched + 2];
            positions[i + 3] = positions[last_untouched + 2];
        }
    }
    void CutFromBottom(int last_untouched, float further_offset)
    {
        if (last_untouched < positions.Length - 2) //lerping
        {
            positions[last_untouched + 1] = Vector3.Lerp(positions[last_untouched], positions[last_untouched + 1], further_offset);
            positions[last_untouched + 3] = Vector3.Lerp(positions[last_untouched + 2], positions[last_untouched + 3], further_offset);
        }

        for (int i = last_untouched + 4; i < positions.Length; i += 4)
        {
            positions[i] = positions[last_untouched];
            positions[i + 1] = positions[last_untouched];
            positions[i + 2] = positions[last_untouched + 2];
            positions[i + 3] = positions[last_untouched + 2];
        }
    }
    void CutFromTop(int last_untouched, float further_offset)
    {
        if (last_untouched < positions.Length - 2) //lerping
        {
            positions[last_untouched + 1] = Vector3.Lerp(positions[last_untouched], positions[last_untouched + 1], further_offset);
            positions[last_untouched - 1] = Vector3.Lerp(positions[last_untouched - 2], positions[last_untouched - 1], further_offset);
        }

        for (int i = last_untouched + 2; i < positions.Length; i += 4)
        {
            positions[i] = positions[last_untouched - 2];
            positions[i + 1] = positions[last_untouched - 2];
            positions[i + 2] = positions[last_untouched];
            positions[i + 3] = positions[last_untouched];
        }
    }
}


[CreateAssetMenu(fileName = "SpriteShapeUVAnimator", menuName = "ScriptableObjects/SpriteShapeUVAnimator", order = 1)]
public class SpriteShapeFill : SpriteShapeGeometryModifier
{
    [Range(0,1)]
    public float t01;

    public float vert_count_mult;

    public override JobHandle MakeModifierJob(JobHandle generator, SpriteShapeController spriteShapeController, NativeArray<ushort> indices,
        NativeSlice<Vector3> positions, NativeSlice<Vector2> texCoords, NativeSlice<Vector4> tangents, NativeArray<UnityEngine.U2D.SpriteShapeSegment> segments, NativeArray<float2> colliderData)
    {
        var mj = new FillJob() { positions = positions, mult = vert_count_mult, t01 = t01 };
        var jh = mj.Schedule(generator);
        return jh;
    }
}


