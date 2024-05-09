using System;
using System.Collections;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class DebugDrawer : MonoBehaviour
    {
        [SerializeField] Color color;
        [SerializeField] bool disabled;

        public void DrawRay(Vector2 start, Vector2 dir)
        {
            if (disabled) return;
            Debug.DrawRay(start, dir, color);
        }

        public IEnumerator DrawRay(Vector2 start, Vector2 dir, float duration)
        {
            if (disabled) yield break;

            float t = 0;
            while(t < duration)
            {
                Debug.DrawRay(start, dir, color);
                yield return null;
                t += Time.deltaTime;
            }
        }

        public IEnumerator DrawRay(Vector2 start, Vector2 dir, float duration, Color c)
        {
            if (disabled) yield break;

            float t = 0;
            while (t < duration)
            {
                Debug.DrawRay(start, dir, c);
                yield return null;
                t += Time.deltaTime;
            }
        }

        public void DrawDot(Vector2 point, float radius, float duration, Color color)
        {
            Gizmos.color = color;
            StartCoroutine(DrawDot(point, radius, duration));
        }

        InnerSphere sphere;
        public IEnumerator DrawDot(Vector2 point, float radius, float duration)
        {
            if (disabled) yield break;
            InnerSphere sphere = new InnerSphere() { pos = point, radius = radius };

            float t = 0;
            while (t < duration)
            {
                this.sphere = sphere;
                yield return null;
                t += Time.deltaTime;
            }
            this.sphere = null;
        }
        private void OnDrawGizmos()
        {
            if(sphere != null)
            {
                sphere.Draw();
            }
        }

        class InnerSphere
        {
            public Vector2 pos;
            public float radius;
            public void Draw()
            {
                Gizmos.DrawSphere(pos, radius);
            }
        }
    }
}