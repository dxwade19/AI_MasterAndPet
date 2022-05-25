using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AP_WayPointSystem))]
public class FSM_WayPointSystemEditor : Editor
{
	AP_WayPointSystem pattern = null;
    SerializedProperty points = null;

    private void OnEnable()
    {
        pattern = (AP_WayPointSystem)target;
        points = serializedObject.FindProperty("waypoints");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("AddPoint"))
            pattern.AddPoint();
        if (GUILayout.Button("Clear points"))
            pattern.Clear();
    }

    private void OnSceneGUI()
    {
        if (points == null) return;
        serializedObject.Update();
        for (int i = 0; i < points.arraySize; i++)
        {
            points.GetArrayElementAtIndex(i).vector3Value = Handles.DoPositionHandle(points.GetArrayElementAtIndex(i).vector3Value,Quaternion.identity);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
