using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataRetrieve))]
public class DayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (DataRetrieve)target;

        if(GUILayout.Button("Load Data", GUILayout.Height(40)))
        {
            script.GetData();
        }

        if(GUILayout.Button("Reset Data", GUILayout.Height(40)))
        {
            script.ResetData();
        }
        
    }
}