using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;
using System.Threading.Tasks;
using System.Threading;

namespace Game
{
    public class Spawner : Script
    {
        public UIControl timerUIControl;
        private Label _timerLabel;
        public List<Wave> _waves = new List<Wave>();
        public AudioSource _spawnSound = null;

        private float _timeBeetweenWaves = 5.5f;
        private float _countdown = 10f;
        private int _waveIndex = 0;

        private SpawnManager _spawnManager = null;

        CancellationTokenSource tokenSource = null;

        public override void OnStart()
        {
            _spawnManager = SpawnManager.GetInstance();
            _timerLabel = (Label)timerUIControl.Control;
        }

        public override void OnUpdate()
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

            _countdown -= Time.DeltaTime;

            _timerLabel.Text = Mathf.Round(_countdown).ToString();
        }

        public async void SpawnWave()
        {
            _spawnSound.Play();

            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //make the spawn infinite
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

        private void SpawnEnemy(Prefab enemyPrefab, CancellationToken token)
        {
            if (this.Actor != null)
            {
                PrefabManager.SpawnPrefab(enemyPrefab, this.Actor.Position);
            }
        }


        public override void OnDisable()
        {
            if (tokenSource != null)
            {
               tokenSource.Cancel();
            }          
        }
    }
}
