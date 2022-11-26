using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private int _health = 200;
    [SerializeField]
    private int _value = 25;
    [SerializeField]
    private int _rotationSpeed = 10;

    [Header("Unity References")]

    private Transform _target = null;
    private int _waypointIndex = 0;
    private float _originalSpeed;
    private bool _isSlowed = false;

    private PlayerStatisticsManager _playerStatisticsManager;
    private SpawnManager _spawnManager;

    void Start()
    {
        _target = Waypoints.waypoints[0];
        _playerStatisticsManager = PlayerStatisticsManager.GetInstance();
        _spawnManager = SpawnManager.GetInstance();
        _originalSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);

        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= 0.1) 
        {
            GetNextWayPoint();
            return;
        }

        Vector3 lookDir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void GetNextWayPoint()
    {
        if (_waypointIndex >= Waypoints.waypoints.Length -1)
        {
            DestinationReached();
            return;
        }

        _waypointIndex++;
        _target = Waypoints.waypoints[_waypointIndex];
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
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

    private void Die()
    {
        //add money
        _playerStatisticsManager.AddMoney(_value);

        Destroy(this.gameObject);
        _spawnManager.EnemyDied();

        //TODO: Death particles?
    }

    private void DestinationReached()
    {
        Destroy(this.gameObject);
        _spawnManager.EnemyDied();
        _playerStatisticsManager.SubtractLives(_damage);
    }
}
