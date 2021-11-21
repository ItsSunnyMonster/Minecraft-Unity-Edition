//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;
using System;

public class GameModeMgr : MonoBehaviour
{
    private static GameModeMgr Instance;

    public static GameMode CurrentGameMode
    {
        get
        {
            return Instance.currentGameMode_intern;
        }
        set
        {
            Instance.OnGameModeSwitch_intern?.Invoke(CurrentGameMode, value);
            Instance.currentGameMode_intern = value;
        }
    }
    private GameMode currentGameMode_intern;

    public static event Action<GameMode, GameMode> OnGameModeSwitch
    {
        add
        {
            Instance.OnGameModeSwitch_intern += value;
        }
        remove
        {
            Instance.OnGameModeSwitch_intern -= value;
        }
    }
    private event Action<GameMode, GameMode> OnGameModeSwitch_intern;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There are more than one instances of GameModeMgr in the scene! ");
        }
        else
        {
            Instance = this;
        }
        currentGameMode_intern = GameMode.CREATIVE;
    }

    public static void SwitchGameMode(GameMode gameMode)
    {
        Instance.SwitchGameMode_impl(gameMode);
    }
    private void SwitchGameMode_impl(GameMode gameMode)
    {
        CurrentGameMode = gameMode;
    }

    public static bool InstanceCreated()
    {
        return Instance != null;
    }

    public enum GameMode
    {
        SPECTATOR, CREATIVE
    }
}