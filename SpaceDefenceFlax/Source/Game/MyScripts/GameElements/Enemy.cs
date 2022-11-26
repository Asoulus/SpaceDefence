using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;

namespace Game
{
    public class Enemy : Script
    {
        public float _speed = 400f;
        public float _health = 100f;
        public int _damage = 1;
        public int _rotationSpeed = 10;
        

        public int _value = 100;

        private Vector3 _target;
        private int wavepointIndex = 0;
        private float _originalSpeed;
        private bool _isSlowed = false;
        private SpawnManager _spawnManager;
        private PlayerStatisticsManager _playerStatisticsManager;
        

        //events
        public delegate void EnemyDeathEventHandler(object source, EventArgs args);
        public event EnemyDeathEventHandler EnemyDeath;

        protected virtual void onEnemyDeath()
        {
            if (EnemyDeath != null)
            {
                EnemyDeath(this, EventArgs.Empty);
            }
        }

        public override void OnStart()
        {
            _originalSpeed = _speed;
            _target = Waypoints.points[0].Position;
            _spawnManager = SpawnManager.GetInstance();
            _playerStatisticsManager = PlayerStatisticsManager.GetInstance();
        }


        public override void OnUpdate()
        {
            if (_health <= 0)
            {
                Die();
                return;
            }

            Vector3 direction = _target - this.Actor.Position;

            this.Actor.AddMovement(direction.Normalized * _speed * Time.DeltaTime);

            if (Vector3.Distance(this.Actor.Position, _target) <= 4f) //increase if gets stuck
            {
                GetNextWaypoint();
            }

            //ROTATION TOWARDS WAYPOINT
            Vector3 lookDirection = _target - this.Actor.Position;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            Vector3 rotation = Quaternion.Lerp(this.Actor.Orientation, lookRotation, _rotationSpeed * Time.DeltaTime).EulerAngles;
            this.Actor.Orientation = Quaternion.Euler(0f, rotation.Y, 0f);
        }

        private void GetNextWaypoint()
        {
            if (wavepointIndex >= Waypoints.points.Length -1)
            {
                DestinationReached();
                return;
            }

            wavepointIndex++;
            _target = Waypoints.points[wavepointIndex].Position;
        }

        public void TakeDamage(float damage)
        {          
            _health -= damage;
        }

        public async void Slow(float slowPercetage, int slowDuration)
        {
            if (_isSlowed)
            {
                return;
            }

            _speed = _speed * (slowPercetage / 100);
            _isSlowed = true;

            await Task.Delay(slowDuration);

            _speed = _originalSpeed;
            _isSlowed = false;
        }

        public void Die()
        {
            //add money
            _playerStatisticsManager.AddMoney(_value);

            Destroy(this.Actor);
            _spawnManager.EnemyDied();
        }

        private void DestinationReached()
        {
            Destroy(this.Actor);
            _spawnManager.EnemyDied();
            _playerStatisticsManager.SubtractLives(_damage);
        }
    }
}
