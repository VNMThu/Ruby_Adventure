using System.Collections;
using UnityEngine;

public class MeleeSoldier : Enemy
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayerMask;
    protected override void OnEnable()
    {
        base.OnEnable();
        animator.SetBool(IsAttacking,false);

    }

    protected override void MovePattern()
    {
        StartCoroutine(MoveToRuby());
    }

    private void Update()
    {
        if (isAlive)
        {
            Vector3 rubyPosition = GameManager.Instance.Ruby.transform.position;
            if (Vector2.Distance(transform.position, rubyPosition) <= attackRange && !isAttacking)
            {
                StartAttack();
            }
            else if (Vector2.Distance(transform.position, rubyPosition) > attackRange && isAttacking)
            {
                StopAttack();
            }
        }
    }

    private IEnumerator MoveToRuby()
    {
        while (isAlive)
        {
            if (!isAttacking)
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
        isAttacking = true;
        animator.SetBool(IsAttacking,true);
    }

    protected override void AttackConnect()
    {
        //Detect if player in range
        Debug.Log("Enemy Attacking");
        var results = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayerMask);
        Debug.Log("Enemy Attacking1:"+results.Length);

        //Hit nothing so return here
        if (results.Length <= 0) return;
        
        //Check all collider
        foreach (var VARIABLE in results)
        {
            if (VARIABLE.CompareTag("Player"))
            {
                //Hit Ruby
                VARIABLE.GetComponent<RubyController>().ChangeHealth(-damage);
            }
        }
    }

    protected override void StopAttack()
    {
        base.StopAttack();
        isAttacking = false;
        animator.SetBool(IsAttacking,false);
    }

    protected override void Death()
    {
        base.Death();
        animator.SetBool(IsAttacking,false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
