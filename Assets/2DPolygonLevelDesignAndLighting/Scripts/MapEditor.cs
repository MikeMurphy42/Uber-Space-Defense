#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapScript))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapScript ms = (MapScript)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update Details"))
        {
            ms.replaceDetails();
        }
        if (GUILayout.Button("Update Paralax"))
        {
            ms.replaceParalax();
        }
        GUILayout.EndHorizontal();
    }
}
#endif