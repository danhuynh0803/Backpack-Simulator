using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapTileEnumScript))]
public class AddMapTileEnum : Editor
{
    MapTileEnumScript myScript;
    string filePath = "Assets/Scripts/";
    string fileName = "MapTileType";

    private void OnEnable()
    {
        myScript = (MapTileEnumScript)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        filePath = EditorGUILayout.TextField("Path", filePath);
        fileName = EditorGUILayout.TextField("Name", fileName);
        if (GUILayout.Button("Save"))
        {
            EditorMethods.WriteToEnum(filePath, fileName, myScript.mapTileName);
            myScript.mapTileName.Sort();
        }

    }
}