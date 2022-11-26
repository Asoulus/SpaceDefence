using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;

    private int _spawnedEnemyAmount = 0;

    [SerializeField]
    private AudioSource _reachedSound = null;

    public int SpawnedEnemyAmount
    {
        get { return _spawnedEnemyAmount; }
        set { _spawnedEnemyAmount =  value; }
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
