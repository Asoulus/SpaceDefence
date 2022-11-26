using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class Shop : Script
    {
        [Header("Flax References")]
        public TurretBlueprint standardTurretBlueprint;
        public TurretBlueprint missileTurretBlueprint;
        public TurretBlueprint slowingTurretBlueprint;

       /* [Tooltip("The menu buttons.")]
        public List<UIControl> Buttons;*/
        
        public UIControl standardTurretControl = null;
        public UIControl missileTurretControl = null;
        public UIControl slowingTurretControl = null;

        private Button standardTurretBtn = null;
        private Button missileTurretBtn = null;
        private Button slowingTurretBtn = null;


        private BuildManager _buildManager;

        public override void OnStart()
        {
            _buildManager = BuildManager.GetInstance();

            AssignButtons();
        }

        private void AssignButtons()
        {
            if (standardTurretControl)
            {
                standardTurretBtn = (Button)standardTurretControl.Control;
                standardTurretBtn.Clicked += BuildStandardTurret;
            }

            if (missileTurretControl)
            {
                missileTurretBtn = (Button)missileTurretControl.Control;
                missileTurretBtn.Clicked += BuildMissleTurret;
            }

            if (slowingTurretControl)
            {
                slowingTurretBtn = (Button)slowingTurretControl.Control;
                slowingTurretBtn.Clicked += BuildSlowingTurret;
            }
        }

        private void BuildStandardTurret()
        {
            _buildManager.SetTurretToBuild(standardTurretBlueprint);
        }
        private void BuildMissleTurret()
        {
            _buildManager.SetTurretToBuild(missileTurretBlueprint);
        }
        private void BuildSlowingTurret()
        {
            _buildManager.SetTurretToBuild(slowingTurretBlueprint);
        }
    }
}
