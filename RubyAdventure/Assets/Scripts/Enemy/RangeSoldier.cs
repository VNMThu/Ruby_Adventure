using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSoldier : Enemy
{
  private readonly int _isAttacking = Animator.StringToHash("IsAttacking");
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private ParticleSystem appearEffect;
    [SerializeField] private float attackRate;
    private readonly int _isDeath = Animator.StringToHash("IsDeath");
    private readonly int _isRunning = Animator.StringToHash("IsRunning");

    protected override void OnEnable()
    {
        base.OnEnable();
        animator.SetBool(_isRunning, false);
        ObjectsPoolManager.SpawnObject(appearEffect.gameObject, transform.position, Quaternion.identity,
            ObjectsPoolManager.PoolType.ParticleSystem);
        StartMoving();
    }

    protected override void StartMoving()
    {
        StartCoroutine(MoveToRuby());
    }

    private void Update()
    {
        //Return if death
        if (!IsAlive) return;
        FlipToFaceRuby();

        Vector3 rubyPosition = GameManager.Instance.Ruby.transform.position;
        if (Vector2.Distance(transform.position, rubyPosition) <= attackRange && !IsAttacking)
        {
            StartAttack();
        }
        else if (Vector2.Distance(transform.position, rubyPosition) > attackRange && IsAttacking)
        {
            StopAttack();
        }
    }

    public void FireBullet()
    {
        Debug.Log("Range Soldier Fire bullet");
    }

    private IEnumerator MoveToRuby()
    {
        while (IsAlive)
        {
            if (!IsAttacking)
            {
                animator.SetBool(_isRunning, true);

                Vector3 rubyPosition = GameManager.Instance.Ruby.transform.position;
                //speed
                float step = speed * Time.deltaTime;

                // move towards the ruby location
                transform.position = Vector2.MoveTowards(transform.position, rubyPosition, step);
            }
            yield return null;
        }
    }

    private void FlipToFaceRuby()
    {
        //Flip sprite depend on ruby position
        if ( GameManager.Instance.Ruby.transform.position.x < transform.position.x)
        {
            foreach (var variable in spriteRenderer)
            {
                variable.flipX = false;
            }
        }
        else
        {
            foreach (var variable in spriteRenderer)
            {
                variable.flipX = true;
            }
        }
    }


    protected override void StartAttack()
    {
        //Stop moving
        animator.SetBool(_isRunning, false);
        
        //Start coroutine attack
        IsAttacking = true;
        StartCoroutine(C_Attack());
    }

    private IEnumerator C_Attack()
    {
        while (IsAttacking)
        {
            animator.SetTrigger(_isAttacking);
            yield return new WaitForSeconds(1 / attackRate);
        }
    }
    

    protected override void StopAttack()
    {
        //Stop attack
        IsAttacking = false;
        
        //Start moving again
        animator.SetBool(_isRunning, true);
    }

    protected override void Death()
    {
        base.Death();
        //Start death animation
        animator.SetTrigger(_isDeath);
    }

    //Get call in animation
    public void ReturnToPool()
    {
        ObjectsPoolManager.ReturnObjectToPool(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
