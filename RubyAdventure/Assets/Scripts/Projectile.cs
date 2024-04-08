using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid_body;
    public AudioClip hitClip;
    private Renderer myRenderer;
    void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100)
        {
            Destroy(gameObject);
        }

    }

    public void Launch(Vector2 direction, float force)
    {

        rigid_body.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            enemyController.PlayAudio(hitClip);
            enemyController.ChangeHp(-1);

        }
        else if (other.tag == "Collectible")
        {
            return;
        }
        Destroy(gameObject);

    }


}
