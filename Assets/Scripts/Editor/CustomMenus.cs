//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CustomMenus
{
    [MenuItem("GameObject/UI/Minecraft/Button")]
    private static void CreateButton()
    {
        GameObject button = null;
        if (Selection.activeTransform == null)
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGameObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvas = canvasGameObject.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform);
        }
        else
        {
            var selection = Selection.activeTransform;
            if (selection.TryGetComponent<Canvas>(out var _) || selection.GetComponentInParent<Canvas>() != null)
            {
                button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), selection);
            }
            else
            {
                Canvas canvas = null;
                for (var i = 0; i < selection.childCount; i++)
                {
                    canvas = selection.GetChild(i).GetComponent<Canvas>();
                    if (canvas != null)
                    {
                        button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform);
                        break;
                    }
                }
                if (canvas == null) 
                {
                    var canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                    canvasObject.transform.SetParent(selection);
                    canvas = canvasObject.GetComponent<Canvas>();
                    button = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform);
                }
            }
        }

        button.name = "Button";

        Selection.activeGameObject = button;


        /*
        var canvas = Selection.activeTransform.GetComponentInChildren<Canvas>(true);
        if (canvas == null)
        {
            var canvasGameObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasGameObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        } 
        Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Button.prefab"), canvas.transform).name = "Button";
        */
    }

    [MenuItem("GameObject/UI/Minecraft/Text")]
    private static void CreateText()
    {
        GameObject text = null;
        if (Selection.activeTransform == null)
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGameObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvas = canvasGameObject.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            text = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Text.prefab"), canvas.transform);
        }
        else
        {
            var selection = Selection.activeTransform;
            if (selection.TryGetComponent<Canvas>(out var _) || selection.GetComponentInParent<Canvas>() != null)
            {
                text = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Text.prefab"), selection);
            }
            else
            {
                Canvas canvas = null;
                for (var i = 0; i < selection.childCount; i++)
                {
                    canvas = selection.GetChild(i).GetComponent<Canvas>();
                    if (canvas != null)
                    {
                        text = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Text.prefab"), canvas.transform);
                        break;
                    }
                }
                if (canvas == null)
                {
                    var canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                    canvasObject.transform.SetParent(selection);
                    canvas = canvasObject.GetComponent<Canvas>();
                    text = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Text.prefab"), canvas.transform);
                }
            }
        }

        text.name = "Text";

        Selection.activeGameObject = text;
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
