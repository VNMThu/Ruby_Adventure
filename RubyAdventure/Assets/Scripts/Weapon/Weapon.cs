using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected float damagePerAttack;
    [SerializeField] protected float forcePushBack;


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
