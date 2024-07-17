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
    [Header("Percentage Drop")]
    [SerializeField] protected float percentageDropExp;
    [Header("EXP Drop")]
    [SerializeField] protected ExpSharp expSharp;
    [Header("Money Drop")]
    [SerializeField] protected CoinCollectible coinPrefab;
    [Header("Flash effect when die")] [SerializeField]
    protected FlashEffect flashEffect;

    protected Rigidbody2D RigidBody2D;
    protected Collider2D EnemyCollider;
    protected float CurrentHealth;
    public bool IsAlive { get; protected set; }
    protected bool IsAttacking;

    protected virtual void OnEnable()
    {
        CurrentHealth = fullHealth;
        IsAlive = true;
        if (RigidBody2D == null)
        {
            RigidBody2D = GetComponent<Rigidbody2D>();
        }
        
        if (EnemyCollider == null)
        {
            EnemyCollider = GetComponent<Collider2D>();
        }

        if (!EnemyCollider.isActiveAndEnabled)
        {
            EnemyCollider.enabled = true;
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
        if (!IsAlive) return;
        CurrentHealth -= damageDeal;
        flashEffect.Flash();
        if (forcePushPower != 0)
        {
            Vector2 force = (transform.position -GameManager.Instance.RubyPosition).normalized;
            RigidBody2D.AddForce(forcePushPower * force, ForceMode2D.Impulse);

            //Apply opposite force to stop 
            DOVirtual.DelayedCall(0.1f, () =>
            {
                RigidBody2D.AddForce(-forcePushPower * force, ForceMode2D.Impulse);
            });
        }

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    //Get Kill
    protected virtual void Death()
    {
        IsAlive = false;
        IsAttacking = false;
        EnemyCollider.enabled = false;
        //Random and spawn exp
        int randomValue = Random.Range(1, 11);
        if (percentageDropExp >= randomValue)
        {
            //Drop it
            ObjectsPoolManager.SpawnObject(expSharp.gameObject, transform.position, Quaternion.identity,
                ObjectsPoolManager.PoolType.Exp);
        }
        else
        {
            Vector3 spawnPosition =
                new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            //Drop it
            ObjectsPoolManager.SpawnObject(coinPrefab.gameObject, spawnPosition, Quaternion.identity);
        }

        

    }
}