using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    public TurretBlueprint basicTurretBlueprint;
    public TurretBlueprint missileTurretBlueprint;
    public TurretBlueprint slowTurretBlueprint;

    private void Start()
    {
        buildManager = BuildManager.GetInstance();
    }

    public void SelectBasicTurret()
    {
        buildManager.SetTurretToBuild(basicTurretBlueprint);
    }

    public void SelectMissileTurret()
    {
        buildManager.SetTurretToBuild(missileTurretBlueprint);
    }

    public void SelectSlowTurret()
    {
        buildManager.SetTurretToBuild(slowTurretBlueprint);
    }
}
