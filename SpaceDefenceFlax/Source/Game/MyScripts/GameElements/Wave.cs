using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    [Serializable]
    public class Wave
    {
        public Prefab enemyPrefab;
        public int enemyCount;
        public int spawnDelayInMiliseconds;
    }
}
