// 
// Copyright (c) SunnyMonster
//

using System;
using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float fpsRefreshRate;
    [SerializeField] private string fpsDisplayFormat = "{0} FPS";
    [SerializeField] private string velocityDisplayFormat = "0.000 b/s";

    private GameModeMgr.GameMode previousGameMode = GameModeMgr.GameMode.CREATIVE;

    private void Start()
    {
        versionText.text = Application.version;
    }

    private void Update()
    {
        UpdateFPS();
        UpdateVelocity();
        HandleDebugInputs();
    }

    private float _timer;
    private void UpdateFPS()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer < fpsRefreshRate) return;
        var fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = string.Format(fpsDisplayFormat, fps);
        _timer = 0;
    }

    private void UpdateVelocity()
    {
        velocityText.text = playerRb.velocity.magnitude.ToString(velocityDisplayFormat);
    }

    private void HandleDebugInputs()
    {
        if (Input.GetKey(KeyCode.F3))
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (GameModeMgr.CurrentGameMode == GameModeMgr.GameMode.SPECTATOR)
                {
                    GameModeMgr.CurrentGameMode = previousGameMode;
                }
                else
                {
                    previousGameMode = GameModeMgr.CurrentGameMode;
                    GameModeMgr.CurrentGameMode = GameModeMgr.GameMode.SPECTATOR;
                }
            }
        }
    }
}
