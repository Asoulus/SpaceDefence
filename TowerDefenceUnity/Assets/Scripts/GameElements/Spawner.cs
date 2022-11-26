using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float _countdown = 2f;
    [SerializeField]
    private float _timeBeetweenWaves = 5.5f;

    [Header("Unity Object References")]
    [SerializeField]
    private List<Wave> _waves = new List<Wave>();
    [SerializeField]
    private Text _countdownText = null;
    [SerializeField]
    private AudioSource _spawnSound = null;

    private CancellationTokenSource _tokenSource = null;
    private SpawnManager _spawnManager = null;
    private int _waveIndex = 0;

    private void Start()
    {
        _spawnManager = SpawnManager.GetInstance();
    }

    private async void SpawnWave()
    {
        _spawnSound.Play();

        _tokenSource = new CancellationTokenSource();
        CancellationToken token = _tokenSource.Token;

        if (_waveIndex >= _waves.Count)
        {
            _waveIndex = 0;
        }

        Wave wave = _waves[_waveIndex];
        _spawnManager.SpawnedEnemyAmount = wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab, token);

            try
            {
                await Task.Delay(wave.spawnDelayInMiliseconds, token);
            }
            catch (OperationCanceledException ex)
            {
                Debug.LogWarning("Cancelation error handled: " + ex);
            }            
        }
        _waveIndex++;
    }

    private void SpawnEnemy(GameObject enemyPrefab, CancellationToken token)
    {
        if (this.gameObject != null)
        {
            Instantiate(enemyPrefab, this.transform);
        }     
    }


    void Update()
    {
        if (_spawnManager.SpawnedEnemyAmount > 0)
        {
            return;
        }

        if (_countdown <= 0)
        {
            SpawnWave();
            _countdown = _timeBeetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;

        _countdownText.text = Mathf.Round(_countdown).ToString();
    }

    private void OnDisable()
    {
        if (_tokenSource != null)
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }     
    }
}
