using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace SpaceDrifter2D
{
    [ExecuteInEditMode]
    public class SplineController : MonoBehaviour
    {
        [SerializeField] SpriteShapeController ss_controller;
        [SerializeField] Spline spline;
        EdgeCollider2D edgeCollider;
        public float impactScale = 1;

        private void Start()
        {
            ss_controller = GetComponent<SpriteShapeController>();
            spline = ss_controller.spline;
        }
        // tests
        public Transform target;
        public float normalizeScale = 5;
        public ShapeTangentMode mode;
        private void OnValidate()
        {
            spline.NormalizeTangents(normalizeScale, mode);
            ss_controller.RefreshSpriteShape();
        }
        private void OnDrawGizmos()
        {
            if (!enabled) return;
            //DrawTests();
            TestNewWay();
        }
        void TestNewWay()
        {
            //if (spline == null)
            //{
                ss_controller = GetComponent<SpriteShapeController>();
                spline = ss_controller.spline;
            //}
            Vector2 point = transform.InverseTransformPoint(target.position);
            int l = spline.GetPointCount();
            Vector2 pos;
            for (int i = 0; i < l - 1; i++)
            {
                pos = spline.ClosestPointOnSegment(i, i + 1, point);
                pos = transform.TransformPoint(pos);

                Gizmos.color = i%2==0?Color.magenta:Color.green;
                Gizmos.DrawSphere(pos, 0.75f);
#if UNITY_EDITOR
                Handles.Label(pos + Vector2.up * 2.5f, Vector2.Distance(pos, target.position).ToString("f2"), new GUIStyle() { fontSize = 20 });
#endif
            }
            //if !openEnded
            pos = spline.ClosestPointOnSegment(l-1, 0, point);
            pos = transform.TransformPoint(pos);

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(pos, 0.75f);
#if UNITY_EDITOR
            Handles.Label(pos + Vector2.up * 2.5f, Vector2.Distance(pos, target.position).ToString("f2"), new GUIStyle() { fontSize = 20 });
#endif
        }

        // old version (normalization based on collider spline points diffrences)
        //void NormalizeTangents()
        //{
        //    ss_controller = GetComponent<SpriteShapeController>();
        //    spline = ss_controller.spline;
        //    edgeCollider = GetComponent<EdgeCollider2D>();

        //    //set normalized tangents
        //    Vector2[] points = new Vector2[spline.GetPointCount()];
        //    points[0] = transform.TransformPoint(spline.GetPosition(0));
        //    for (int i = 0; i < points.Length; i++)
        //    {
        //        points[i] = transform.TransformPoint(spline.GetPosition(i));
        //    }
        //    Vector3 leftT, rightT;
        //    for (int i = 1; i < points.Length - 1; i++)
        //    {
        //        Vector2 coll_point = edgeCollider.ClosestPoint(points[i]);
        //        Vector2 surf_normal = coll_point - points[i];

        //        SplineUtility.CalculateTangents(points[i], points[i - 1], points[i + 1], surf_normal, 4, out rightT, out leftT);
        //        spline.SetLeftTangent(i, leftT);
        //        spline.SetRightTangent(i, rightT);
        //    }
        //    SplineUtility.CalculateTangents(points[0], points[points.Length - 1], points[1], edgeCollider.ClosestPoint(points[0]) - points[0], 4, out rightT, out leftT);
        //    spline.SetLeftTangent(0, leftT);
        //    spline.SetRightTangent(0, rightT);
        //    SplineUtility.CalculateTangents(points[points.Length - 1], points[points.Length - 2], points[0], edgeCollider.ClosestPoint(points[points.Length - 1]) - points[points.Length - 1], 4, out rightT, out leftT);
        //    spline.SetLeftTangent(points.Length - 1, leftT);
        //    spline.SetRightTangent(points.Length - 1, rightT);
        //    //need to test it
        //}

        public float minInsertDist = 5;
        public float minRelVel = 3;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
            if (collision.relativeVelocity.magnitude < minRelVel) return;

            var contact = collision.contacts[0];
            Hit_new(contact.point, contact.normal, impactScale, minInsertDist); //* contact.relativeVelocity.sqrMagnitude *);
        }

        IEnumerator Draw(Vector2 pos, Vector2 dir, float duration)
        {
            float t = 0;
            while(t < duration)
            {
                Debug.DrawLine(pos, pos + dir, Color.cyan);
                yield return null;
                t += Time.deltaTime;
            }
        }
        private void Hit_new(Vector2 point, Vector2 normal, float impactScale, float minInsertDistance)
        {
            // spline.ClosestPointOnPath()
            StartCoroutine(Draw(point, normal, 2));
            //spline.GetPointCount();
            point = transform.InverseTransformPoint(point);
            normal = transform.InverseTransformDirection(normal);

            int[] segment;
            Vector2 closest_p = spline.ClosestPointOnPath(point, out segment, true);//, 1.98f);
            //float dist = Vector2.Distance(point, closest_p);
            Vector2 p0, p1;
            p0 = spline.GetPosition(segment[0]);
            p1 = spline.GetPosition(segment[1]);

            //need to change this, bcuz further points gets moved sometimes
            if(Vector2.Distance(p0, point) < minInsertDistance)
            {
                //just move the closest
                spline.SetPosition(segment[0], point - normal * impactScale);
                spline.SetTangentMode(segment[0], ShapeTangentMode.Continuous);
            }
            else if(Vector2.Distance(p1,point) < minInsertDistance)
            {
                //just move the closest
                spline.SetPosition(segment[1], point - normal * impactScale);
                spline.SetTangentMode(segment[1], ShapeTangentMode.Continuous);
            }
            else  // spline hit far from any point - we place new point between to closest
            {
                //select proper index (points must be in order)
                int insert_index;
                if ((segment[0] == 0 && segment[1] == spline.GetPointCount() - 1) || (segment[1] == 0 && segment[0] == spline.GetPointCount() - 1)) // if new point is between last and first
                    insert_index = spline.GetPointCount();
                else
                    insert_index = Mathf.Max(segment[0], segment[1]);

                //insertion
                spline.InsertPointAt(insert_index, point - normal * impactScale);
                spline.SetTangentMode(insert_index, ShapeTangentMode.Continuous);
                Vector3 leftT, rightT;
                SplineUtility.CalculateTangents(spline.GetPosition(insert_index), spline.GetPosition(insert_index > 0 ? insert_index - 1 : spline.GetPointCount() - 1), spline.GetPosition(insert_index < spline.GetPointCount() - 1 ? insert_index + 1 : 0), normal, normalizeScale, out rightT, out leftT);
                spline.SetLeftTangent(insert_index, leftT);
                spline.SetRightTangent(insert_index, rightT);
            }

            //move also points that are close so the shape doesn't collapse
            // int[] close_points = FindCloseOnes(Vector2 point, clo)

            ss_controller.RefreshSpriteShape();
        }
        public void Hit_old(Vector2 pos, Vector2 dir, float force)
        {
            StartCoroutine(Draw(pos, dir, 2));
            //spline.GetPointCount();
            Vector2 point = transform.InverseTransformPoint(pos);
            Vector2 normal = transform.InverseTransformDirection(dir);

            int closest, scnd_closest;
            Vector3 closest_p = spline.FindClosestIndex(point, out closest);
            scnd_closest = spline.FindScndClosest(point, closest);

            float dist = Vector2.Distance(point, closest_p);

            if (dist > 5) // spline hit far from any point - we place new point between to closest
            {
                //select proper index (points must be in order)
                int insert_index;
                if ((closest == 0 && scnd_closest == spline.GetPointCount() - 1) || (scnd_closest == 0 && closest == spline.GetPointCount() - 1)) // if new point is between last and first
                    insert_index = spline.GetPointCount();
                else
                    insert_index = Mathf.Max(closest, scnd_closest);

                //insertion
                spline.InsertPointAt(insert_index, point + dir * force);
                spline.SetTangentMode(insert_index, ShapeTangentMode.Continuous);
                Vector3 leftT, rightT;
                SplineUtility.CalculateTangents(spline.GetPosition(insert_index), spline.GetPosition(insert_index > 0 ? insert_index - 1 : spline.GetPointCount() - 1), spline.GetPosition( insert_index < spline.GetPointCount() - 1 ? insert_index + 1 : 0), normal, 4, out rightT, out leftT);
                spline.SetLeftTangent(insert_index, leftT);
                spline.SetRightTangent(insert_index, rightT);
            }
            else // spline was hit close to a point - we move it
            {
                //just move the closest
                spline.SetPosition(closest, point + normal * force);
                spline.SetTangentMode(closest, ShapeTangentMode.Continuous);
            }
            //move also points that are close so the shape doesn't collapse
           // int[] close_points = FindCloseOnes(Vector2 point, clo)

            ss_controller.RefreshSpriteShape();
        }
    }
}
