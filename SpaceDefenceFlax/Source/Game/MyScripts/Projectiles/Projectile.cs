using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class Projectile : Script
    {
        [Header("Base Projectile Attributes")]       
        public float _speed = 2f;
        public int _damage = 2;

        [Header("Base Projectile Flax Object References")]
        public Prefab _hitParticleSystem = null;
        public AudioSource _hitSound = null;
        protected Actor _target = null;

        public override void OnUpdate()
        {
            if (!_target)
            {
                Debug.Log("No Target");
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

        protected virtual void HitTarget()
        {
            //custom method per projectile
        }

        public void SetTarget(Actor target)
        {
            _target = target;
        }
    }
}
