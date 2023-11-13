using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GridManager))]
[CanEditMultipleObjects]
public class GridManagerEditor : Editor
{
    SerializedProperty gridSizeX;
    SerializedProperty gridSizeY;
    SerializedProperty gridScale;

    private void OnEnable()
    {
        gridSizeX = serializedObject.FindProperty("SizeX");
        gridSizeY = serializedObject.FindProperty("SizeY");
        gridScale = serializedObject.FindProperty("Scale");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GridManager grid = (GridManager)target;

        GUILayout.Label("Grid Settings", EditorStyles.largeLabel);
        GUILayout.Label("Grid Size", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(gridSizeX, new GUIContent("Size X"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(gridSizeY, new GUIContent("Size Y"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(gridScale, new GUIContent("Scale"), GUILayout.Height(20));

        serializedObject.ApplyModifiedProperties();
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NotInSelectionHierarchy)]
    static void DrawGridGizmo(GridManager _grid, GizmoType gizmoType)
    {
        for (int i = 0; i < _grid.SizeX; ++i)
            for (int j = 0; j < _grid.SizeY; ++j)
                Gizmos.DrawSphere((_grid.transform.position + (new Vector3(i, 0, j) * _grid.Scale)) - new Vector3((_grid.SizeX - 1) * _grid.Scale * 0.5f, 0, (_grid.SizeY - 1) * _grid.Scale * 0.5f), 0.25f);
    }
}
#endif