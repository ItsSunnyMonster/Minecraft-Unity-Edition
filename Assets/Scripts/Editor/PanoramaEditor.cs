//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Panorama))]
public class PanoramaEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var panorama = (Panorama) target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set Textures"))
        {
            panorama.SetTextures();
        }
        if (GUILayout.Button("Reset Textures"))
        {
            panorama.ResetTextures();
        }
        GUILayout.EndHorizontal();
    }
}