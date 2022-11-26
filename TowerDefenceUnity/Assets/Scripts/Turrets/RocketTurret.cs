using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : Turret
{
    [Header("Rocket Turret Unity Object References")]
    [SerializeField]
    protected GameObject _firePoint;
    [SerializeField]
    protected GameObject _projectilePrefab;

    protected override void Shoot()
    {
        GameObject tmp = Instantiate(_projectilePrefab, _firePoint.transform);
        Projectile Projectile = tmp.GetComponent<Projectile>();

        //Play sound
        _fireSound.Play();

        if (Projectile != null)
        {
            Projectile.SetTarget(_target.gameObject);
        }
    }
}
