//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayfabManager))]
public class PlayfabManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Delete Log Files"))
        {
            var path = Application.persistentDataPath + "/Logs/PlayFab/";
            if (Directory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);
                foreach (var file in dirInfo.EnumerateFiles())
                {
                    file.Delete();
                }
            }
            Debug.Log("Log files deleted! ");
        }
        if (GUILayout.Button("Open Log Directory"))
        {
#if UNITY_EDITOR_WIN
            Debug.Log("Opening Explorer...");
#elif UNITY_EDITOR_OSX
            Debug.Log("Opening Finder...");
#endif
            EditorUtility.RevealInFinder(Application.persistentDataPath + "/Logs/PlayFab/");
        }
        GUILayout.EndHorizontal();
    }
}