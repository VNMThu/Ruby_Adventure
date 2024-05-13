using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidBody;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private ParticleSystem sparkHit;
    private float _damageDeal;
    private float _forcePushBack;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (transform.position.magnitude > 100)
        {
            ObjectsPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void OnDisable()
    {
        _rigidBody.velocity = Vector3.zero;
    }

    public void Launch(Vector2 direction, float force, float damageDealValue, float forcePushBackValue)
    {
        _rigidBody.AddForce(direction * force, ForceMode2D.Impulse);
        _damageDeal = damageDealValue;
        _forcePushBack = forcePushBackValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
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
                enemyController.GetHitNormal(_damageDeal, _forcePushBack);

                ObjectsPoolManager.ReturnObjectToPool(gameObject);

                break;
            }
        }
    }
}