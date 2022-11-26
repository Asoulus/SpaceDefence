using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class Bullet : Script
    {
        public ParticleSystem hitParticleSystem;

        private Actor _target;
        private float _speed = 1000f;      

        public override void OnUpdate()
        {
            if (!_target) 
            {
                Destroy(this.Actor);
                return;
            }

            Vector3 direction = _target.Position - this.Actor.Position;
            float distanceThisFrame = _speed * Time.DeltaTime;
          
            if (direction.Length <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            this.Actor.AddMovement(direction.Normalized * _speed * Time.DeltaTime);
        }

        private void HitTarget()
        {
            if (_target == null)
            {

                Destroy(this.Actor);
                return;
            }

            Actor effect = hitParticleSystem.Spawn(Actor.Transform);
            Destroy(effect, 1.5f);

            //deal damage to enemy
            _target.GetScript<Enemy>().TakeDamage(50f);

            Destroy(this.Actor);                    
            return;
        }

        public void SetTarget(Actor target)
        {
            _target = target;
        }
    }
}
