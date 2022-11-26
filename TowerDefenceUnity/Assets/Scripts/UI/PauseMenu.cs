using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Attributes")]
    private bool _isPaused = true;

    [Header("Unity References")]
    [SerializeField]
    private LevelLoader _levelLoader = null;
    [SerializeField]
    private CanvasGroup _pauseMenu = null;
  

    void Start()
    {
        TogglePause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            _pauseMenu.alpha = 1;
            _pauseMenu.interactable = true;
            _pauseMenu.blocksRaycasts = true;
            Time.timeScale = 0f;
        }
        else
        {
            _pauseMenu.alpha = 0;
            _pauseMenu.interactable = false;
            _pauseMenu.blocksRaycasts = false;
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        TogglePause();
    }

    public void Restart()
    {
        _levelLoader.PrepareForReloading();
    }

    public void MainMenu()
    {
        _levelLoader.PrepareForLoading("Menu");
    }
}
