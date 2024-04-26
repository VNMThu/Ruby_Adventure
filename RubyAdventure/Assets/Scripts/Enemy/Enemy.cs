using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fullHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer[] spriteRenderer;

    protected float currentHealth;
    protected bool isAlive;
    protected bool isAttacking;

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
    protected virtual void StartAttack()
    {
    }

    protected virtual void StopAttack()
    {
        
    }

    // This function use for checking attack hit box, should be call inside attack animation for exact frame checking
    protected virtual void AttackConnect()
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
}
