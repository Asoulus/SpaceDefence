using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class PlayerStatisticsManager : Script
    {
        private static PlayerStatisticsManager _instance;

        private int _money;
        private int _lives;
        private GameManager _gameManager;

        [ShowInEditor]
        private int _startingMoney = 500;
        
        [ShowInEditor]
        private int _startingLives = 15;

        public TextRender _livesDisplayText;
        public TextRender _moneyDisplayText;

        public override void OnStart()
        {
            _money = _startingMoney;
            _lives = _startingLives;

            _moneyDisplayText.Text = "$" + _money.ToString();
            _livesDisplayText.Text = _lives.ToString() + " LIVES";

            _gameManager = GameManager.GetInstance();
        }

        public override void OnAwake()
        {
            SetInstance();
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

        public static PlayerStatisticsManager GetInstance()
        {
            return _instance;
        }

        public int GetMoneyAmount()
        {
            return _money;
        }

        public void SubtractMoney(int money)
        {
            _money -= money;
            _moneyDisplayText.Text = "$" + _money.ToString();
        }

        public void AddMoney(int money)
        {
            _money += money;
            _moneyDisplayText.Text = "$" + _money.ToString();
        }

        public void SubtractLives(int lives)
        {
            _lives -= lives;
            _lives = Mathf.Clamp(_lives, 0, _startingLives);
            _livesDisplayText.Text = _lives.ToString() + " LIVES";

            if (_lives <= 0)
            {
                _gameManager.EndGame();
            }
        }
    }
}
