using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class StandardTurret : Turret
    {
        [Header("Standard Turret Flax Object References")]
        public Actor _firePoint;
        public Prefab _projectilePrefab;

        protected override void Shoot()
        {
            Actor tmp = PrefabManager.SpawnPrefab(_projectilePrefab, _firePoint.Position);
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
