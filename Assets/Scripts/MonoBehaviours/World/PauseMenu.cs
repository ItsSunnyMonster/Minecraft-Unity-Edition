// 
// Copyright (c) SunnyMonster
//

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private UnityEvent m_OnPause;
    [SerializeField] private UnityEvent m_OnUnpause;

    public event Action OnPause;
    public event Action OnUnpause;

    private bool _isPaused = false;

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Pause();
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (_isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pauseMenu.blocksRaycasts = true;
        pauseMenu.interactable = true;
        _isPaused = true;
        OnPause?.Invoke();
        m_OnPause?.Invoke();
    }

    public void Unpause()
    {
        pauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pauseMenu.blocksRaycasts = false;
        pauseMenu.interactable = false;
        _isPaused = false;
        OnUnpause?.Invoke();
        m_OnUnpause?.Invoke();
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndex.Menu);
    }
}
