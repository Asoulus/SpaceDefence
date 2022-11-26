using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class SpawnManager : Script
    {
        private static SpawnManager _instance;

        private int _spawnedEnemyAmount = 0;

        public AudioSource _reachedSound = null;

        public int SpawnedEnemyAmount
        {
            get { return _spawnedEnemyAmount; }
            set { _spawnedEnemyAmount = value; }
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

        public static SpawnManager GetInstance()
        {
            return _instance;
        }

        public void EnemyDied()
        {
            _reachedSound.Play();
            SpawnedEnemyAmount--;
        }
    }
}
