using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fullHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer[] spriteRenderer;

    protected float currentHealth;
    protected bool isAlive;

    protected virtual void OnEnable()
    {
        currentHealth = fullHealth;
        isAlive = true;
        MovePattern();
    }

    //Move Pattern
    protected virtual void MovePattern()
    {
    }
    
    //Attack
    protected virtual void Attack()
    {
    }

    protected virtual void StopAttack()
    {
        
    }
    
    //React when hit by different kind of attack
    public virtual void GetHitNormal(float damageDeal)
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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Attack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StopAttack();
    }
}
