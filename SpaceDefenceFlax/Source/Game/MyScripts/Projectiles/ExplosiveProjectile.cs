using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{   
    public class ExplosiveProjectile : Projectile
    {
        [Header("Explosive Projectile Attributes")]
        public float _explosionRadius = 5f;

        [Header("Explosive Projectile Unity Object References")]
        public LayersMask enemyLayer;

        protected override void HitTarget()
        {
            //spawn explosion effect           
            Actor effect = PrefabManager.SpawnPrefab(_hitParticleSystem, this.Actor.Transform);
            Destroy(effect, 1.5f);

            //deal damage          
            Physics.OverlapSphere(this.Actor.Position, _explosionRadius, out Collider[] results, enemyLayer, false);

            Collider[] enemiesInRange = results;

            if (enemiesInRange.Length <= 0)
            {
                _target = null;
                Destroy(this.Actor);
                return;
            }          

            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                enemiesInRange[i].GetScript<Enemy>().TakeDamage(_damage);
            }

            /*//disable to play sound
            transform.GetChild(0).gameObject.SetActive(false);

            //play sound
            _hitSound.Play();*/

            //destroy 
            Destroy(this.Actor);
        }

        /*public override void OnDebugDrawSelected()
        {
            BoundingSphere boundingSphere = new BoundingSphere(this.Actor.Position, _explosionRadius);
            DebugDraw.DrawWireSphere(boundingSphere, Color.Red);
        }*/
    }
}
