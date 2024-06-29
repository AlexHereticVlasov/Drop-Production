using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelLoader levelLoader = (LevelLoader)target;
        if (GUILayout.Button("Save"))
            levelLoader.Save();
        if (GUILayout.Button("Load"))
            levelLoader.Load();
    }
}
