using UnityEngine;

public class RubyGunBullet : Projectile
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private ParticleSystem sparkHit;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
            {
                Enemy enemyController = other.GetComponent<Enemy>();

                //Spawn Particle
                ObjectsPoolManager.SpawnObject(sparkHit.gameObject, transform.position, transform.rotation,
                    ObjectsPoolManager.PoolType.ParticleSystem);

                //Play audio
                // enemyController.PlayAudio(hitClip);

                //Change HP
                enemyController.GetHitNormal(DamageDeal, ForcePushBack);

                ObjectsPoolManager.ReturnObjectToPool(gameObject);

                break;
            }
        }
    }
}