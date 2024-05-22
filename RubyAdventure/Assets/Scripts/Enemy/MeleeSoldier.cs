using System.Collections;
using UnityEngine;

public class MeleeSoldier : Enemy
{
    private readonly int _isAttacking = Animator.StringToHash("IsAttacking");
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private ParticleSystem appearEffect;
    private readonly int _isDeath = Animator.StringToHash("IsDeath");

    protected override void OnEnable()
    {
        base.OnEnable();
        animator.SetBool(_isAttacking, false);
        ObjectsPoolManager.SpawnObject(appearEffect.gameObject, transform.position, Quaternion.identity,
            ObjectsPoolManager.PoolType.ParticleSystem);
    }

    protected override void StartMoving()
    {
        StartCoroutine(MoveToRuby());
    }

    private void Update()
    {
        if (IsAlive)
        {
            Vector3 rubyPosition = GameManager.Instance.Ruby.transform.position;
            if (Vector2.Distance(transform.position, rubyPosition) <= attackRange && !base.IsAttacking)
            {
                StartAttack();
            }
            else if (Vector2.Distance(transform.position, rubyPosition) > attackRange && base.IsAttacking)
            {
                StopAttack();
            }
        }
    }

    private IEnumerator MoveToRuby()
    {
        while (IsAlive)
        {
            if (!base.IsAttacking)
            {
                Vector3 rubyPosition = GameManager.Instance.Ruby.transform.position;
                //speed
                float step = speed * Time.deltaTime;

                // move towards the ruby location
                transform.position = Vector2.MoveTowards(transform.position, rubyPosition, step);

                //Flip sprite depend on ruby position
                if (rubyPosition.x < transform.position.x)
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

            yield return null;
        }
    }


    protected override void StartAttack()
    {
        base.StartAttack();

        //Play animation attack
        base.IsAttacking = true;
        animator.SetBool(_isAttacking, true);
    }

    protected override void AttackConnect()
    {
        //Detect if player in range
        var results = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayerMask);

        //Hit nothing so return here
        if (results.Length <= 0) return;

        //Check all collider
        foreach (var variable in results)
        {
            if (variable.CompareTag("Player"))
            {
                //Hit Ruby
                variable.GetComponent<RubyController>().ChangeHealth(-damage);
            }
        }
    }

    protected override void StopAttack()
    {
        base.StopAttack();
        base.IsAttacking = false;
        animator.SetBool(_isAttacking, false);
    }

    protected override void Death()
    {
        base.Death();
        animator.SetBool(_isAttacking, false);
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}