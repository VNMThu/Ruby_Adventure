using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fullHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer[] spriteRenderer;
    [Header("EXP Drop")] [SerializeField] protected float percentageDrop;
    [SerializeField] protected ExpSharp expSharp;

    [Header("Flash effect when die")] [SerializeField]
    protected FlashEffect flashEffect;

    protected Rigidbody2D rigidbody2D;
    protected float currentHealth;
    public bool IsAlive { get; private set; }
    protected bool isAttacking;

    protected virtual void OnEnable()
    {
        currentHealth = fullHealth;
        IsAlive = true;
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    //Move Pattern - This will get call inside animation
    protected virtual void StartMoving()
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
    public virtual void GetHitNormal(float damageDeal, float forcePushPower = 0f)
    {
        currentHealth -= damageDeal;
        flashEffect.Flash();
        if (forcePushPower != 0)
        {
            Vector2 force = (transform.position - GameManager.Instance.Ruby.transform.position).normalized;
            rigidbody2D.AddForce(forcePushPower * force, ForceMode2D.Impulse);

            //Apply opposite force to stop 
            DOVirtual.DelayedCall(0.1f, () => { rigidbody2D.AddForce(-forcePushPower * force, ForceMode2D.Impulse); });
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    //Get Kill
    protected virtual void Death()
    {
        IsAlive = false;
        isAttacking = false;
        //Random and spawn exp
        int randomValue = Random.Range(1, 11);
        if (percentageDrop >= randomValue)
        {
            //Drop it
            ObjectsPoolManager.SpawnObject(expSharp.gameObject, transform.position, Quaternion.identity,
                ObjectsPoolManager.PoolType.Exp);
        }
    }
}