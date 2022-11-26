using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class BuildManager : Script
    {     
        public static BuildManager _instance;

        private TurretBlueprint _turretToBuild;
        private PlayerStatisticsManager _playerStatisticsManager;

        public bool CanBuild { get { return _turretToBuild != null; } }

        public AudioSource errorSound = null;
        public AudioSource buildSound = null;
        public Prefab buildEffect = null;

        public override void OnAwake()
        {
            SetInstance();
        }

        public override void OnStart()
        {
            _playerStatisticsManager = PlayerStatisticsManager.GetInstance();
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

        public static BuildManager GetInstance()
        {
            return _instance;
        }

        public void BuildTurretAtNode(Node node)
        {

            if (_playerStatisticsManager.GetMoneyAmount() < _turretToBuild.cost)
            {
                errorSound.Play();
                return;
            }

            _playerStatisticsManager.SubtractMoney(_turretToBuild.cost);

            Actor tmpTurret = PrefabManager.SpawnPrefab(_turretToBuild.prefab, node.Actor.Position + new Vector3(0, 5, 0), Quaternion.Identity);
            node.Turret = tmpTurret;

            //play sound
            buildSound.Play();

            //Spawne effect
            Actor effect = PrefabManager.SpawnPrefab(buildEffect, node.Actor.Position);
            Destroy(effect, 2f);
        }

        public  void SetTurretToBuild(TurretBlueprint turret)
        {
            _turretToBuild = turret;
        }
    }
}
