using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Enemy
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
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
            if (Vector2.Distance(transform.position, rubyPosition) <= detectedRange && !isAttacking)
            {
                Attack();
            }
            else if (Vector2.Distance(transform.position, rubyPosition) > detectedRange && isAttacking)
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
    
    
    
    protected override void Attack()
    {
        base.Attack();
        isAttacking = true;
        animator.SetBool(IsAttacking,true);
        Debug.Log("Enemy Attacking");
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
    }
    
    
}
