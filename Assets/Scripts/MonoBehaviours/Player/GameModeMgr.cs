//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public enum GameMode
{
    Spectator,
    Creative
}

namespace MonoBehaviours.Player
{
    public class GameModeMgr : MonoBehaviour
    {
        public GameMode currentGameMode = GameMode.Spectator;
        private GameMode _previousGameMode = GameMode.Creative;

        public GameModeComponentDictionary gameModeComponents = new GameModeComponentDictionary();

        /// <summary>
        /// Switch the game mode to <paramref name="mode"/>
        /// </summary>
        /// <param name="mode">The game mode to switch to</param>
        public void Switch(GameMode mode)
        {
            // It mode didn't change then do nothing
            if (mode == currentGameMode) return;
            
            // Loop through all the components and disabling them
            foreach (var behaviour in gameModeComponents[currentGameMode].componentList)
            {
                behaviour.enabled = false;
            }
            
            // Loop through all the components that should be enabled and enabling them
            foreach (var behaviour in gameModeComponents[mode].componentList)
            {
                behaviour.enabled = true;
            }

            // Keep track of previous game mode
            _previousGameMode = currentGameMode;
            
            // Set the current game mode to the mode switched to
            currentGameMode = mode;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.F3) && Input.GetKeyDown(KeyCode.N))
            {
                Switch(_previousGameMode);
            }
        }

        /// <summary>
        /// The serializable dictionary
        /// </summary>
        [Serializable]
        public class GameModeComponentDictionary : SerializableDictionaryBase<GameMode, ComponentList> {}

        /// <summary>
        /// The wrapper for List
        /// </summary>
        [Serializable]
        public struct ComponentList
        {
            public List<MonoBehaviour> componentList;
        }
    }
}