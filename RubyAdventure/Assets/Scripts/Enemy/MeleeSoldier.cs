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

    private IEnumerator MoveToRuby()
    {
        while (isAlive)
        {
            //speed
            float step = speed * Time.deltaTime;

            // move towards the ruby location
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.Ruby.transform.position, step);
    
            //Flip sprite depend on ruby position
            if (GameManager.Instance.Ruby.transform.position.x < transform.position.x)
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
            yield return null;
        }
    } 
    
    
    
    protected override void Attack()
    {
        base.Attack();
        animator.SetBool(IsAttacking,true);
        Debug.Log("Enemy Attacking");
    }

    protected override void StopAttack()
    {
        base.StopAttack();
        animator.SetBool(IsAttacking,false);
    }

    protected override void Death()
    {
        base.Death();
    }
    
    
}
