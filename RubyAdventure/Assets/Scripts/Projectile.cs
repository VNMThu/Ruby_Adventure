using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidBody;
    public AudioClip hitClip;

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
        _rigidBody.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
            {
                EnemyController enemyController = other.GetComponent<EnemyController>();
                enemyController.PlayAudio(hitClip);
                enemyController.ChangeHp(-1);
                break;
            }
            case "Collectible":
                return;
        }

        ObjectsPoolManager.ReturnObjectToPool(gameObject);
    }


}
