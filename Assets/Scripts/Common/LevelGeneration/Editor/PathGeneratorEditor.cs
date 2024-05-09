using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceDrifter2D
{
    [CustomEditor(typeof(PathGenerator))]
    public class PathGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Next"))
            {
                PathGenerator g = target as PathGenerator;
                g.PlaceSegment();
            }
        }
    }
}
