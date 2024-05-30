using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float forcePushBack;
    protected WeaponAttribute currentAttribute;
    [SerializeField] protected WeaponType type;
    protected float attackCountDown;
    private Action<object> _onWeaponUpgrade;


    private void OnEnable()
    {
        //Handle when a upgrade is chosen
        _onWeaponUpgrade = (param) => SetAttribute((WeaponData)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnWeaponUpgrade, _onWeaponUpgrade);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.OnWeaponUpgrade,_onWeaponUpgrade);
    }


    private void SetAttribute(WeaponData data)
    {
        if (type == data.Type)
        {
            currentAttribute = data.Attribute;    
        }
        
    }

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