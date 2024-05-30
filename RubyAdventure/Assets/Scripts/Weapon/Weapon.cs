using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected int damagePerAttack;
    [SerializeField] protected float forcePushBack;
    protected float attackCountDown;


    public virtual void Attack()
    {
    }

    public virtual void StopAttack()
    {
    }

    public virtual void FlipSprite()
    {
    }
}