// 
// Copyright (c) SunnyMonster
//

using System;
using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    public TextMeshProUGUI versionText;
    public TextMeshProUGUI fpsText;
    public float fpsRefreshRate;
    public string fpsDisplayFormat = "{0} FPS";

    private void Start()
    {
        versionText.text = Application.version;
    }

    private float _timer;
    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer < fpsRefreshRate) return;
        var fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = string.Format(fpsDisplayFormat, fps);
        _timer = 0;
    }
}
