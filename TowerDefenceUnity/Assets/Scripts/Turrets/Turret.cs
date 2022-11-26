using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Base Turret Attributes")]
    [SerializeField]
    protected float _range = 12.5f;
    [SerializeField]
    protected float _rotationSpeed = 10;
    [SerializeField]
    protected float _fireRate = 1f;

    protected Transform _target;
    protected float _findingCountdown = .5f;
    protected float _fireCountdown = 0f;
    

    [Header("Base Turret Unity Object References")]
    [SerializeField]
    protected GameObject _partToRotate;
    [SerializeField]
    protected LayerMask _enemyLayer;
    [SerializeField]
    protected AudioSource _fireSound;

    void Update()
    {
        if (_findingCountdown <= 0)
        {
            UpdateTarget();
            _findingCountdown = .5f;
        }

        _findingCountdown -= Time.deltaTime;
        _fireCountdown -= Time.deltaTime;

        if (_target == null)
        {
            return;         
        }

        LookTowardsEnemy();

        if (_fireCountdown <= 0)
        {
            Shoot();
            _fireCountdown = 1f / _fireRate;
        }
    }

    protected virtual void LookTowardsEnemy()
    {
        //ROTATION TOWARDS ENEMY
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_partToRotate.transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        _partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected virtual void Shoot()
    {
        //custom method per turret
    }

    protected virtual void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position,_range, _enemyLayer);     

        if (enemiesInRange.Length <= 0)
        {
            _target = null;
            return;
        }

        GameObject[] targets = new GameObject[enemiesInRange.Length];
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            targets[i] = enemiesInRange[i].gameObject;
        }

        foreach (GameObject target in targets)
        {
            float distanceToEnemy = Vector3.Distance(this.transform.position, target.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = target;
            }

            if (nearestEnemy != null && shortestDistance <= _range)
            {
                _target = nearestEnemy.transform;
            }
            else
            {
                _target = null;
            }
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _range);      
    }
}
