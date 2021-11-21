// 
// Copyright (c) SunnyMonster
//

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameModeMgr))]
public class GameModeMgrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GameModeMgr.InstanceCreated())
            GameModeMgr.CurrentGameMode = (GameModeMgr.GameMode)EditorGUILayout.EnumPopup(GameModeMgr.CurrentGameMode);
    }
}
