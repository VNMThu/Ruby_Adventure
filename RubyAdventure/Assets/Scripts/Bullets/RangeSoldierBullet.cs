using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSoldierBullet : Projectile
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private ParticleSystem sparkHit;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case Constant.RubyHurtBoxTag:
            {
                //Spawn Particle
                ObjectsPoolManager.SpawnObject(sparkHit.gameObject, transform.position, transform.rotation,
                    ObjectsPoolManager.PoolType.ParticleSystem);

                //Play audio
                // enemyController.PlayAudio(hitClip);

                //Change HP
                GameManager.Instance.Ruby.ChangeHealth(-DamageDeal);

                ObjectsPoolManager.ReturnObjectToPool(gameObject);

                break;
            }
        }
    }
}
