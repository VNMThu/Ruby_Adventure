using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidBody;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private ParticleSystem sparkHit;
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

    public void Launch(Vector2 direction, float force)
    {
        _rigidBody.AddForce(direction * force,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter Trigger:"+other.gameObject.name);
        switch (other.tag)
        {
            case "Enemy":
            {
                EnemyController enemyController = other.GetComponent<EnemyController>();
                
                //Spawn Particle
                ObjectsPoolManager.SpawnObject(sparkHit.gameObject, transform.position, transform.rotation,
                    ObjectsPoolManager.PoolType.ParticleSystem);
                
                //Play audio
                enemyController.PlayAudio(hitClip);
                
                //Change HP
                // enemyController.ChangeHp(-1);
                
                ObjectsPoolManager.ReturnObjectToPool(gameObject);

                break;
            }
        }

    }


}
