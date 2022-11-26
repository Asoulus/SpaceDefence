using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticsManager : MonoBehaviour
{
    private static PlayerStatisticsManager _instance;

    private int _money;
    private int _lives;
    private GameManager _gameManager;

    [SerializeField]
    private int _startingMoney = 400;
    [SerializeField]
    private Text _moneyDisplayText;   
    [SerializeField]
    private int _startingLives = 25;
    [SerializeField]
    private Text _livesDisplayText;

    private void Start()
    {
        _money = _startingMoney;
        _lives = _startingLives;

        _moneyDisplayText.text = "$" + _money.ToString();
        _livesDisplayText.text = _lives.ToString() + " LIVES";

        _gameManager = GameManager.GetInstance();
    }

    public void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
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
        _moneyDisplayText.text = "$" + _money.ToString();
    }

    public void AddMoney(int money)
    {
        _money += money;
        _moneyDisplayText.text = "$" + _money.ToString();
    }

    public void SubtractLives(int lives)
    {
        _lives -= lives;
        _lives = Mathf.Clamp(_lives, 0, _startingLives);
        _livesDisplayText.text = _lives.ToString() + " LIVES";

        if (_lives <= 0)
        {
            _gameManager.EndGame();
        }
    }
}
