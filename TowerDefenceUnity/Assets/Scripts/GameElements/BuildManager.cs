using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static BuildManager _instance;

    [Header("Unity References")]
    [SerializeField]
    private AudioSource _errorSound = null;
    [SerializeField]
    private AudioSource _buildSound = null;  
    [SerializeField]
    private GameObject _buildEffect = null;

    private TurretBlueprint _turretToBuild;
    private PlayerStatisticsManager _playerStatisticsManager;
    public bool CanBuild { get { return _turretToBuild != null; } }


    public void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        _playerStatisticsManager = PlayerStatisticsManager.GetInstance();
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

    public static BuildManager GetInstance()
    {
        return _instance;
    }

    public void BuildTurretOnNode(Node node)
    {
        if (_playerStatisticsManager.GetMoneyAmount() < _turretToBuild.cost)
        {
            _errorSound.Play();
            return;
        }

        _playerStatisticsManager.SubtractMoney(_turretToBuild.cost);

        //create turret
        GameObject tmp = Instantiate(_turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.SetTurret(tmp);

        _buildSound.Play();

        //spawn build effect
        GameObject effect = Instantiate(_buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void SetTurretToBuild(TurretBlueprint turret)
    {
        _turretToBuild = turret;
    }
}

