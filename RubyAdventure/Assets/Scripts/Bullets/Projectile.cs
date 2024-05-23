using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody2D RigidBody;
    protected int DamageDeal;
    protected float ForcePushBack;

    protected void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    protected void Update()
    {
        if (transform.position.magnitude > 100)
        {
            ObjectsPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    protected void OnDisable()
    {
        RigidBody.velocity = Vector3.zero;
    }

    public virtual void Launch(Vector2 direction, float force, int damageDealValue, float forcePushBackValue)
    {
        RigidBody.AddForce(direction * force, ForceMode2D.Impulse);
        DamageDeal = damageDealValue;
        ForcePushBack = forcePushBackValue;
    }
    

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
}
