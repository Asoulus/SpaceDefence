using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class BulletProjectile : Projectile
    {
        protected override void HitTarget()
        {          
            if (!_target)
            {
                Destroy(this.Actor);
                return;
            }

            //spawn hit effect
            Actor effect = PrefabManager.SpawnPrefab(_hitParticleSystem, this.Actor.Position);
            Destroy(effect, 1.5f);

            //deal damage to enemy
            _target.GetScript<Enemy>().TakeDamage(_damage);

            Destroy(this.Actor);
            return;
        }
    }
}
