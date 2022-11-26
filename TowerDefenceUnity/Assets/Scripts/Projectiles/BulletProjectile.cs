using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    protected override void HitTarget()
    {
        //spawn effect
        GameObject effect = Instantiate(_hitParticleSystem, this.transform.position, this.transform.rotation);
        Destroy(effect, 1.5f);

        //deal damage
        _target.GetComponent<Enemy>().TakeDamage(_damage);

/*        //disable to play sound
        transform.GetChild(0).gameObject.SetActive(false);

        //play sound
        _hitSound.Play();*/

        //destroy 
        Destroy(this.gameObject);
    }
}
