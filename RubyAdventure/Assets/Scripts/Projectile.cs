using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidBody.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.collider.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
}
