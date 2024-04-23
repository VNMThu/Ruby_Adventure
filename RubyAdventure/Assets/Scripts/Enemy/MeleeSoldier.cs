using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldier : Enemy
{
    private bool _isAlive;

    protected override void MovePattern()
    {
        base.MovePattern();
    }

    private IEnumerator MoveToRuby()
    {
        while (_isAlive)
        {
            //speed
            float step = speed * Time.deltaTime;

            // move towards the ruby location
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.Ruby.transform.position, step);
            yield return null;
        }
    } 
    
    
    
    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Death()
    {
        base.Death();
    }
    
    
}
