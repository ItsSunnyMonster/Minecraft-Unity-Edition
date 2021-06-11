//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public static class CustomMenus
    {
        [MenuItem("GameObject/UI/Minecraft/Button")]
        private static void CreateButton()
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                canvas = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)).GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            } 
            Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform).name = "Button";
        }

        [MenuItem("GameObject/UI/Minecraft/Text")]
        private static void CreateText()
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                canvas = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)).GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            } 
            Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Text.prefab"), canvas.transform).name = "Text";
        }

        [MenuItem("Tools/SunnyMonster/Open Persistent Data Path")]
        private static void OpenPersistentPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("Tools/SunnyMonster/Open Data Path")]
        private static void OpenDataPath()
        {
            EditorUtility.RevealInFinder(Application.dataPath);
        }
    }
}