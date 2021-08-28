//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using UnityEngine;

public enum GameMode
{
    Spectator,
    Creative
}

public class GameModeMgr : MonoBehaviour
{
    public GameMode currentGameMode = GameMode.Spectator;
    public event Action<GameMode> OnSwitchGameMode;
    
    private GameMode _previousGameMode = GameMode.Creative;

    /// <summary>
    /// Switch the game mode to <paramref name="mode"/>
    /// </summary>
    /// <param name="mode">The game mode to switch to</param>
    public void Switch(GameMode mode)
    {
        // It mode didn't change then do nothing
        if (mode == currentGameMode) return;

        // Keep track of previous game mode
        _previousGameMode = currentGameMode;
        
        // Set the current game mode to the mode switched to
        currentGameMode = mode;
        
        OnSwitchGameMode?.Invoke(mode);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F3) && Input.GetKeyDown(KeyCode.N))
        {
            Switch(_previousGameMode);
        }
    }
}