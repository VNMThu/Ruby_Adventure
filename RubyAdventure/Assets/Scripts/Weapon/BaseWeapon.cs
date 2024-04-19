using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected float damagePerAttack;

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
