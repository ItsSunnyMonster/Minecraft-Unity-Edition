//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonoBehaviours.Menu
{
    public enum SceneIndex
    {
        Menu,
        Game
    }

    public class GameManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("World");
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Quited");
        }
    }
}