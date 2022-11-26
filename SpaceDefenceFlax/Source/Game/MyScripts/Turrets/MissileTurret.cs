using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class MissileTurret : Turret
    {
        [Header("Rocket Turret Flax Object References")]
        public Actor _firePoint;
        public Prefab _projectilePrefab;

        protected override void Shoot()
        {
            Actor tmp = PrefabManager.SpawnPrefab(_projectilePrefab, _firePoint);
            Projectile projectile = tmp.FindScript<Projectile>();

            //Play sound
            fireSound.Play();

            if (projectile != null)
            {
                projectile.SetTarget(_target);
            }
        }
    }
}
