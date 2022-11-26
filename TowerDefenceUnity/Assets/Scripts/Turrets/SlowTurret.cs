using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTurret : Turret
{
    [Header("Slow Turret Attributes")]
    [SerializeField]
    protected float _slowPercentage = 15f;
    [SerializeField]
    protected int _slowDuration = 2000;
    [SerializeField]
    protected int _damageAmount = 15;


    [Header("Slow Turret Unity Object References")]
    [SerializeField]
    protected GameObject _slowingParticle;

    protected override void Shoot()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(this.transform.position, _range, _enemyLayer);

        //Spawn particle
        GameObject effect = Instantiate(_slowingParticle, this.transform);
        Destroy(effect, 1f);

        //Play sound
        _fireSound.Play();

        if (enemiesInRange.Length <= 0)
        {
            _target = null;
            return;
        }

        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            Enemy tmpEnemy = enemiesInRange[i].GetComponent<Enemy>();
            tmpEnemy.TakeDamage(_damageAmount);
            tmpEnemy.Slow(_slowPercentage, _slowDuration);
        }
    }

    protected override void LookTowardsEnemy()
    {
        return;
    }
}
