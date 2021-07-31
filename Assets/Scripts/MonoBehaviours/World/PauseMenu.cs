// 
// Copyright (c) SunnyMonster
//

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup pauseMenu;

    private bool isPaused_;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused_)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pauseMenu.blocksRaycasts = true;
        pauseMenu.interactable = true;
    }

    public void Unpause()
    {
        pauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pauseMenu.blocksRaycasts = false;
        pauseMenu.interactable = false;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndex.Menu);
    }
}
