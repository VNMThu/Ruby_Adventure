using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed; 
    [SerializeField] protected float damage;
    [SerializeField] protected float fullHealth;
    [SerializeField] protected float speed;
    protected float currentHealth;
    protected bool isAlive;

    protected virtual void OnEnable()
    {
        currentHealth = fullHealth;
        isAlive = true;
    }

    //Move Pattern
    protected virtual void MovePattern()
    {
    }
    
    //Attack
    protected virtual void Attack()
    {
    }
    
    //React when hit by different kind of attack
    protected virtual void GetHitNormal(float damageDeal)
    {
        currentHealth -= damageDeal;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    //Get Kill
    protected virtual void Death()
    {
        isAlive = false;
        ObjectsPoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Attack();
    }
}
