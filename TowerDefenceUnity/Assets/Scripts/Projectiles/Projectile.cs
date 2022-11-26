using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Base Projectile Attributes")]
    [SerializeField]
    protected float _speed = 50f;
    [SerializeField]
    protected int _damage = 50;

    [Header("Base Projectile Unity Object References")]
    [SerializeField]
    protected GameObject _hitParticleSystem = null;
    [SerializeField]
    protected AudioSource _hitSound = null;
    protected GameObject _target = null;

    void Update()
    {
        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = _target.transform.position - this.transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
    }

    protected virtual void HitTarget()
    {
        //custom method per projectile
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
