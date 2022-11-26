using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [Header("Unity References")]
    [SerializeField]
    private CanvasGroup _gameOverScreen;

    private bool _isGameOver = false;

    public void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        _gameOverScreen.alpha = 0;
        _gameOverScreen.interactable = false;
        _gameOverScreen.blocksRaycasts = false;
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

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public void EndGame()
    {
        _gameOverScreen.alpha = 1;
        _gameOverScreen.interactable = true;
        _gameOverScreen.blocksRaycasts = true;

        _isGameOver = true;
        Time.timeScale = 0f;   
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
}
