using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class PauseMenu : Script
    {
        [Header("Attributes")]
        private bool _isPaused = true;
        public bool isPauseMenu = true;

        [Header("Flax References")]
        public UIControl pauseControl = null;
        public UIControl resumeControl = null;
        public UIControl restartControl = null;
        public UIControl mainMenuControl = null;
        public SceneReference mainMenuScene;
        public SceneReference curretScene;
        public AudioSource pressSound;

        private LevelLoader _levelLoader = null;
        private Panel _pauseMenu = null;
        private Button _resumeButton = null;
        private Button _restartButton = null;
        private Button _mainMenuButton = null;
        

        public override void OnStart()
        {
            _pauseMenu = (Panel)pauseControl.Control;
            _levelLoader = LevelLoader.GetInstance();

            AssignButtons();

            TogglePause();
        }

        private void AssignButtons()
        {
            if (resumeControl)
            {
                _resumeButton = (Button)resumeControl.Control;
                _resumeButton.Clicked += Resume;
            } 

            if (restartControl)
            {
                _restartButton = (Button)restartControl.Control;
                _restartButton.Clicked += Restart;
            } 
            
            if (mainMenuControl)
            {
                _mainMenuButton = (Button)mainMenuControl.Control;
                _mainMenuButton.Clicked += MainMenu;
            }                                      
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyboardKeys.Escape))
            {
                TogglePause();
            }         
        }

        private void TogglePause()
        {
            if (!isPauseMenu)
            {
                return;
            }

            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _pauseMenu.Visible = true;
                _pauseMenu.Enabled = true;
                Time.TimeScale = 0f;
            }
            else
            {
                _pauseMenu.Visible = false;
                _pauseMenu.Enabled = false;
                Time.TimeScale = 1f;
            }
        }

        public void Resume()
        {           
            TogglePause();
            pressSound.Play();
        }

        public void Restart()
        {
            pressSound.Play();
            _levelLoader.PrepareForLoading(curretScene);       
        }

        public void MainMenu()
        {
            pressSound.Play();
            _levelLoader.PrepareForLoading(mainMenuScene);
        }

    }
}
