using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class Node : Script
    {
        private Actor _turret = null;
        private BuildManager _buildManager;
        public Actor Turret
        {
            get => _turret;
            set => _turret = value;
        }

        private Vector3 _positionOffset = new Vector3(0, 0.5f, 0);

        public override void OnStart()
        {
            _buildManager = BuildManager.GetInstance();
        }       

        public void BuildMyTurret()
        {
            //is turret seleced
            if (!_buildManager.CanBuild)
            {
                Debug.Log("TODO: Display no turret selected");
                return;
            }
                

            //does turret already exist on this node
            if (Turret != null)
            {
                Debug.Log("TODO display: turret already exists on this node");
                return;
            }

            _buildManager.BuildTurretAtNode(this);
        }

        public Vector3 GetBuildPosition()
        {
            return Actor.Position + _positionOffset;
        }
    }
}
