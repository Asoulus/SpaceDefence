using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class GameManager : Script
    {
        private static GameManager _instance;

        [Header("Flax References")]
        public UIControl gameOver;

        private bool _isGameOver = false;
        private Panel _gameOverScreen;

        public override void OnAwake()
        {
            SetInstance();
        }

        public override void OnStart()
        {
            _gameOverScreen = (Panel)gameOver.Control;

            _gameOverScreen.Visible = false;
        }

        private void SetInstance()
        {
            if (_instance != null)
            {
                Destroy(this.Actor);
                return;
            }

            _instance = this;
        }

        public static GameManager GetInstance()
        {
            return _instance;
        }

        public void EndGame()
        {
            _isGameOver = true;
            Time.TimeScale = 0f;

            _gameOverScreen.Visible = true;
        }

        public bool IsGameOver()
        {
            return _isGameOver;
        }
    }
}
