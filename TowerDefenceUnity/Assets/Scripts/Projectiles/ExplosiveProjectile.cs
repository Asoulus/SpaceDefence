using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [Header("Explosive Projectile Attributes")]
    [SerializeField]
    protected float _explosionRadius = 5f;

    [Header("Explosive Projectile Unity Object References")]
    [SerializeField]
    protected LayerMask _enemyLayer;

    protected override void HitTarget()
    {
        //spawn effect
        GameObject effect = Instantiate(_hitParticleSystem, this.transform.position, this.transform.rotation);
        Destroy(effect, 1.5f);

        //deal damage
        Collider[] enemiesInRange = Physics.OverlapSphere(this.transform.position, _explosionRadius, _enemyLayer);

        /*//disable to play sound
        transform.GetChild(0).gameObject.SetActive(false);

        //play sound
        _hitSound.Play();*/

        if (enemiesInRange.Length <= 0)
        {
            _target = null;
            Destroy(this.gameObject);
            return;
        }

        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            Enemy tmpEnemy = enemiesInRange[i].GetComponent<Enemy>();
            tmpEnemy.TakeDamage(_damage);
        }

        //destroy 
        Destroy(this.gameObject);
    }

    /*protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _explosionRadius);
    }*/
}
