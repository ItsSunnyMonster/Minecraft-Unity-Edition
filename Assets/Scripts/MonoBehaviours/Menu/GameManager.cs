//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;
using UnityEngine.SceneManagement;

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