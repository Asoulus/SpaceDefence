using System;
using System.Collections.Generic;
using FlaxEngine;


namespace Game
{
    public class Turret : Script
    {
        [Header("Base Turret Attributes")]
        public float range = 500;
        public float fireRate = 1f;
        public float rotationSpeed = 10f;

        [Header("Base Turret Flax Referneces")]
        public Actor partToRotate;
        public LayersMask enemyLayer;
        public AudioSource fireSound;

        protected Actor _target;
        protected float _findingCountdown = .25f;
        protected float _fireCountdown = 0f;     
        

        public override void OnUpdate()
        {
            if (_findingCountdown <= 0)
            {
                UpdateTarget();
                _findingCountdown = .25f;
            }           

            _findingCountdown -= Time.DeltaTime;
            _fireCountdown -= Time.DeltaTime;

            if (!_target)
            {
                return;
            }

            LookTowardsEnemy();

            if (_fireCountdown <= 0)
            {
                Shoot();
                _fireCountdown = 1f / fireRate;
            }          
        }

        protected virtual void LookTowardsEnemy()
        {
            if (!_target)
            {
                return;
            }

            //ROTATION TOWARDS ENEMY
            Vector3 direction = _target.Position - this.Actor.Position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(partToRotate.Orientation, lookRotation, rotationSpeed * Time.DeltaTime).EulerAngles;
            partToRotate.Orientation = Quaternion.Euler(0f, rotation.Y, 0f);
        }

        protected virtual void Shoot()
        {
            //custom per turret
        }

        protected virtual void UpdateTarget()
        {
            float shortestDistance = 1000000;
            Actor nearestEnemy = null;

            Physics.OverlapSphere(this.Actor.Position, range, out Collider[] results, enemyLayer, false);
     
            Collider[] enemiesInRange = results;

            if (enemiesInRange.Length <= 0)
            {
                _target = null;
                return;
            }

            Actor[] targets = new Actor[enemiesInRange.Length];
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                targets[i] = enemiesInRange[i].As<Actor>();
            }


            foreach (Actor enemy in targets)
            {
                float distanceToEnemy = Vector3.Distance(this.Actor.Position, enemy.Position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }

                if (nearestEnemy != null && shortestDistance <= range)
                {
                    _target = nearestEnemy;
                }
                else
                {
                    _target = null;
                }
            }
        }

        public override void OnDebugDrawSelected()
        {
            BoundingSphere boundingSphere = new BoundingSphere(this.Actor.Position, range);
            DebugDraw.DrawWireSphere(boundingSphere, Color.Red);
        }
    }
}
