using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class SlowingTurret : Turret
    {
        [Header("Slow Turret Attributes")]
        public float _slowPercentage = 15f;
        public int _slowDuration = 2000;
        public int _damageAmount = 15;


        [Header("Slow Turret Unity Object References")]
        public Prefab slowingParticle;

        protected override void Shoot()
        {
            Physics.OverlapSphere(this.Actor.Position, range, out Collider[] results, enemyLayer, false);

            Collider[] enemiesInRange = results;

            //spawn slowing effect           
            Actor effect = PrefabManager.SpawnPrefab(slowingParticle, this.Actor.Position);
            Destroy(effect, 1f);

            //Play sound
            fireSound.Play();

            if (enemiesInRange.Length <= 0)
            {
                _target = null;
                return;
            }

            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                Enemy tmpEnemy = enemiesInRange[i].GetScript<Enemy>();
                tmpEnemy.TakeDamage(_damageAmount);
                tmpEnemy.Slow(_slowPercentage, _slowDuration);
            }
        }

        protected override void LookTowardsEnemy()
        {
            return;
        }

    }
}
