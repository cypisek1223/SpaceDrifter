using SpaceDrifter2D;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceDrifter2D
{
    [CustomEditor(typeof(Movert))]
    public class MoverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Move"))
            {
                Movert mover = target as Movert;
                mover.Move();
            }
        }
    }
}