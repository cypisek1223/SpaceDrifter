using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace SpaceDrifter2D
{
    public static class SplineLerperExtension
    {
        //https://forum.unity.com/threads/how-to-move-an-object-along-a-spline-spriteshape.837739/#post-7036966
        /// <summary>
        /// Returns the local position along the spline based on progress 0 - 1.
        /// Good for lerping an object along the spline.
        /// <para></para>
        /// Example: transform.localPosition = spline.GetPoint(0.5f)
        /// </summary>
        /// <param name="spline"></param>
        /// <param name="progress">Value from 0 - 1</param>
        /// <returns></returns>
        public static Vector2 GetPoint(this Spline spline, float progress, bool openEnded = true)
        {
            int length;
            if (openEnded)
                length = spline.GetPointCount() - 1;
            else
                length = spline.GetPointCount();

            var i = Mathf.Clamp(Mathf.CeilToInt((length) * progress), 0, length);

            var t = progress * (length) % 1f;
            if (i == length && progress >= 1f)
                t = 1;

            var prevIndex = Mathf.Max(i - 1, 0);

            if (!openEnded && i == length)
                i = 0;

            var _p0 = new Vector2(spline.GetPosition(prevIndex).x, spline.GetPosition(prevIndex).y);
            var _p1 = new Vector2(spline.GetPosition(i).x, spline.GetPosition(i).y);
            var _rt = _p0 + new Vector2(spline.GetRightTangent(prevIndex).x, spline.GetRightTangent(prevIndex).y);
            var _lt = _p1 + new Vector2(spline.GetLeftTangent(i).x, spline.GetLeftTangent(i).y);

            return BezierUtility.BezierPoint(
               new Vector2(_p0.x, _p0.y),
               new Vector2(_rt.x, _rt.y),
               new Vector2(_lt.x, _lt.y),
               new Vector2(_p1.x, _p1.y),
               t
            );
            // old is wrong idk why
            //return BezierUtility.BezierPoint(
            //   new Vector2(_rt.x, _rt.y),
            //   new Vector2(_p0.x, _p0.y),
            //   new Vector2(_p1.x, _p1.y),
            //   new Vector2(_lt.x, _lt.y),
            //   t
            //);
        }
        public static Vector2 ClosestPointOnPathPrune( this Spline spline, Vector2 point, out int[] segment, bool closedShape, float minPruneDistance) // stop searching when you find point close enough
        {
            segment = new int[2];
            Vector2 closest = spline.GetPosition(0);
            for (int i = 0; i < spline.GetPointCount() - 1; i++)
            {
                Vector2 pos = spline.ClosestPointOnSegment(i, i + 1, point);
                float dist = Vector2.Distance(pos, point);
                if (dist < Vector2.Distance(closest, point))
                {
                    closest = pos;
                    segment[0] = i; segment[1] = i + 1;
                    if(dist <= minPruneDistance)
                    {
                        return closest;
                    }
                }
            }

            if (closedShape)
            {
                Vector2 pos = spline.ClosestPointOnSegment(spline.GetPointCount() - 1, 0, point);
                if (Vector2.Distance(pos, point) < Vector2.Distance(closest, point))
                {
                    closest = pos;
                    segment[0] = spline.GetPointCount() - 1; segment[1] = 0;
                }
            }
            return closest;
        }

        public static Vector2 ClosestPointOnPath( this Spline spline, Vector2 point, out int[] segment, bool closedShape)
        {
            segment = new int[2];
            Vector2 closest = spline.GetPosition(0);
            for(int i=0; i<spline.GetPointCount()-1; i++)
            {
                Vector2 pos = spline.ClosestPointOnSegment( i, i + 1, point);
                if(Vector2.Distance(pos, point) < Vector2.Distance(closest, point))
                {
                    closest = pos;
                    segment[0] = i; segment[1] = i + 1;
                }
            }

            if(closedShape)
            {
                Vector2 pos = spline.ClosestPointOnSegment(spline.GetPointCount()-1, 0, point);
                if (Vector2.Distance(pos, point) < Vector2.Distance(closest, point))
                {
                    closest = pos;
                    segment[0] = spline.GetPointCount()-1; segment[1] = 0;
                }
            }
            return closest;
        }

        public static Vector2 ClosestPointOnSegment(this Spline spline, int segmentStart, int segmentEnd, Vector2 point) // segments must be neighbours
        {
            Vector2 bezierStart = spline.GetPosition(segmentStart);
            Vector2 startRightTan = bezierStart + (Vector2)spline.GetRightTangent(segmentStart);
            Vector2 bezierEnd = spline.GetPosition(segmentEnd);
            Vector2 endLeftTan = bezierEnd + (Vector2)spline.GetLeftTangent(segmentEnd);

            float t;
            Vector2 close = BezierUtility_2023.ClosestPointOnCurveFast(point, bezierStart, bezierEnd, startRightTan, endLeftTan, 0.05f, out t);
            //Vector2 closest = BezierUtility.BezierPoint(bezierStart, startRightTan, endLeftTan, bezierEnd, t);

            return close;
        }

        public static Vector3 FindClosestIndex(this Spline spline, Vector3 localPos, out int index)
        {
            index = 0;
            float closestDistance = Vector2.Distance(localPos, spline.GetPosition(0));
            Vector3 closestPos = Vector3.zero;
            for (int i = 1; i < spline.GetPointCount(); i++)
            {
                Vector3 pos = spline.GetPosition(i);
                float d = Vector2.Distance(pos, localPos);
                if (d < closestDistance)
                {
                    index = i;
                    closestDistance = d;
                    closestPos = pos;
                }
            }

            return closestPos;
        }

        public static int FindScndClosest(this Spline spline, Vector2 point, int closest_i) //considers only closed shapes. might need to update that later
        {
            int count = spline.GetPointCount();
            int[] others = new int[2];

            if (closest_i == 0)
            {
                others[0] = count - 1;
                others[1] = 1;
            }
            else if (closest_i == count - 1)
            {
                others[0] = count - 2;
                others[1] = 0;
            }
            else
            {
                others[0] = closest_i - 1;
                others[1] = closest_i + 1;
            }

            Vector2 closest_to_point = (point - (Vector2)spline.GetPosition(closest_i)).normalized;
            Vector2 closest_to_left = (spline.GetPosition(others[0]) - spline.GetPosition(closest_i)).normalized;
            Vector2 closest_to_right = (spline.GetPosition(others[1]) - spline.GetPosition(closest_i)).normalized;
            int scnd_closest = others[0];
            if (Vector2.Dot(closest_to_point, closest_to_right) > Vector2.Dot(closest_to_point, closest_to_left))
            {
                scnd_closest = others[1];
            }

            return scnd_closest;
        }

        public static void NormalizeTangents(this Spline spline, float scale, ShapeTangentMode mode)
        {
            //set normalized tangents
            int pointCount = spline.GetPointCount();
            Vector3 leftT, rightT;
            Vector2 surf_normal;
            for (int i = 1; i < pointCount - 1; i++)
            {
                surf_normal = spline.CalcNormal(i);//need normalization
                SplineUtility.CalculateTangents(spline.GetPosition(i), spline.GetPosition(i - 1), spline.GetPosition(i + 1), surf_normal, scale, out rightT, out leftT);
                spline.SetLeftTangent(i, leftT);
                spline.SetRightTangent(i, rightT);
                spline.SetTangentMode(i, mode);
            }
            surf_normal = spline.CalcNormal(0);
            SplineUtility.CalculateTangents(spline.GetPosition(0), spline.GetPosition(pointCount - 1), spline.GetPosition(1), surf_normal, scale, out rightT, out leftT);
            spline.SetLeftTangent(0, leftT);
            spline.SetRightTangent(0, rightT);
            spline.SetTangentMode(0, mode);
            surf_normal = spline.CalcNormal(pointCount-1);
            SplineUtility.CalculateTangents(spline.GetPosition(pointCount-1), spline.GetPosition(pointCount -2), spline.GetPosition(0), surf_normal, scale, out rightT, out leftT);
            spline.SetLeftTangent(pointCount - 1, leftT);
            spline.SetRightTangent(pointCount - 1, rightT);
            spline.SetTangentMode(pointCount - 1, mode);
            //need to test it
        }
        public static Vector2 CalcNormal(this Spline spline, int i)
        {
            Vector3 tan = spline.GetRightTangent(i);
            return Vector3.Cross(tan, Vector3.forward).normalized;
        }
    } 
}
