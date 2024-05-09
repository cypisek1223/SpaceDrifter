using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpaceDrifter2D
{
    [CustomEditor(typeof(MeshLayer))]
    public class MeshLayerEditor : Editor
    {
        SerializedProperty layer;
        int selected;

        private void OnEnable()
        {
            layer = serializedObject.FindProperty("layerIdx");
            selected = layer.intValue;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            base.OnInspectorGUI();
           // MeshLayer t = target as MeshLayer;
            selected = EditorGUILayout.Popup(selected, SortingLayer.layers.Select( l => l.name ).ToArray());
            //t.SetSortLayer(selected);
            layer.intValue = selected;

            serializedObject.ApplyModifiedProperties();
        }
    } 
}
