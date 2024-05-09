using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public static class FixPSDImporter
    {
#if UNITY_EDITOR
        //[UnityEditor.InitializeOnLoadMethod]
        public static void ResetPSDImporterFoldout()
        {
            UnityEditor.EditorPrefs.DeleteKey("PSDImporterEditor.m_PlatformSettingsFoldout");
        } 
#endif
    } 
} 