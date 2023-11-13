using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Unit))]
public class UnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Unit unit = (Unit)target;
        if (unit == null) return;
        else if(unit.Settings == null)
        {
            GUILayout.Label("Unit is not set up!");
        }
        else
        {
            GUILayout.Label("Unit Statistics", EditorStyles.largeLabel);
            //------------------ UNIT STATISTICS ------------------
            GUILayout.BeginHorizontal();
            //Health
            GUILayout.Label("HP: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.Health.ToString(), EditorStyles.boldLabel);
            //Attack
            GUILayout.Label("ATK: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.Attack.ToString(), EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            //AttackSpeed
            GUILayout.Label("ATKSPD: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.AttackSpeed.ToString(), EditorStyles.boldLabel);
            //Speed
            GUILayout.Label("SPD: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.Speed.ToString(), EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            GUILayout.Space(5.0f);

            //------------------ UNIT SHAPE / SIZE / COLOR ------------------
            GUILayout.Label("Shape / Size", EditorStyles.largeLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Size: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.Size.ToString(), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Shape: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.Shape.ToString(), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Color: ", GUILayout.ExpandWidth(false));
            GUILayout.Label(unit.Settings.Color.ToString(), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }
    }
}
#endif