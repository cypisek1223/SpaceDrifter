using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class BezierTest : MonoBehaviour
{
    [Range(0,1)]
    public float t;
    Transform left, right, left_c, right_c;

    public Transform target;
    public Transform source;

    private void Start()
    {
        if (left != null && right != null && left_c != null && right_c != null) return;

        left = new GameObject("left").transform;
        right = new GameObject("right").transform;
        left_c = new GameObject("left_c").transform;
        right_c = new GameObject("right_c").transform;

        left.parent = right.parent = transform;
        left_c.parent = left;
        right_c.parent = right;

        left.localPosition = Vector2.left * 2;
        right.localPosition = Vector2.right * 2;

        left_c.localPosition = Vector2.up;
        right_c.localPosition = Vector2.up;
    }

    public float f;
    private void Update()
    {
        // LTBezierPath // for later
        target.position = BezierUtility.BezierPoint(left.position, left_c.position, right_c.position, right.position, t);


        BezierUtility_2023.ClosestPointOnCurve(source.position, left.position, right.position, left_c.position, right_c.position, 0.05f, out f);

    }
}
