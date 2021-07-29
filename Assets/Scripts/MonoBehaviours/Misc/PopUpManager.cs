//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using System.Collections;
using System.Collections.Generic;
using Dependencies.Rotary_Heart.SerializableDictionaryLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance { get; private set; }
    
    private Queue<PopUp> _popUpQueue = new Queue<PopUp>();

    [Space]
    [Header("References")]
    public GameObject popup;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public Image iconImage;

    [Space]
    [Header("Options")] 
    public float animationTime = 1f;
    public float animationStayingTime = 3f;
    public bool useAnimationCurve;
    public AnimationCurve easeCurve;
    public LeanTweenType easeMode;
    
    [Space] 
    [Header("Icon Presets")]
    public IconPresetDictionary iconPresets;
    

    private void Awake()
    {
        // Set singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (_popUpQueue.Count > 0)
            {
                // Set up pop up
                var popUp = _popUpQueue.Dequeue();
                titleText.text = popUp.Title;
                contentText.text = popUp.Content;
                if (popUp.Icon == null)
                {
                    iconImage.gameObject.SetActive(false);
                }
                else
                {
                    iconImage.gameObject.SetActive(true);
                    iconImage.sprite = popUp.Icon;
                }
                
                // Get rectTransform component
                var rectTrans = popup.GetComponent<RectTransform>();

                yield return null;

                // Set position
                rectTrans.anchoredPosition = new Vector2(rectTrans.rect.width, rectTrans.anchoredPosition.y);

                // Tween pop up into view
                var canvasGroup = popup.GetComponent<CanvasGroup>();

                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                if (useAnimationCurve)
                {
                    LeanTween.moveX(rectTrans, -8, animationTime).setEase(easeCurve);
                }
                else
                {
                    LeanTween.moveX(rectTrans, -8, animationTime).setEase(easeMode);
                }

                // Wait for move to finish
                yield return new WaitForSeconds(animationTime);
                
                // Wait for 3 seconds after moving
                yield return new WaitForSeconds(animationStayingTime);
                
                // Tween pop up back out of view
                if (useAnimationCurve)
                {
                    LeanTween.moveX(rectTrans, rectTrans.rect.width, animationTime).setEase(easeCurve);
                }
                else
                {
                    LeanTween.moveX(rectTrans, rectTrans.rect.width, animationTime).setEase(easeMode);
                }

                // Wait for move to complete and hide popup
                yield return new WaitForSeconds(animationTime);
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;
            }

            yield return null;
        }
    }

    public void PopUp(string title = "", string content = "", Sprite icon = null)
    {
        PopUp(new PopUp(title, content, icon));
    }

    public void PopUp(string iconID, string title = "", string content = "")
    {
        PopUp(title, content, iconPresets[iconID]);
    }

    public void PopUp(PopUp popUp)
    {
        _popUpQueue.Enqueue(popUp);
    }
}

[Serializable]
public class IconPresetDictionary : SerializableDictionaryBase<string, Sprite> {}

public struct PopUp
{
    public readonly string Title;
    public readonly string Content;
    public readonly Sprite Icon;

    public PopUp(string title = "", string content = "", Sprite icon = null)
    {
        Title = title;
        Content = content;
        Icon = icon;
    }

    public PopUp(string iconID, string title = "", string content = "")
    {
        Title = title;
        Content = content;
        Icon = PopUpManager.Instance.iconPresets[iconID];
    }
}