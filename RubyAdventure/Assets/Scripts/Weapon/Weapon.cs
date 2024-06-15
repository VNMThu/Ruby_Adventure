using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float forcePushBack;
    protected WeaponAttribute currentAttribute;
    [SerializeField] protected WeaponType type;
    public WeaponType WeaponType => type;
    protected float attackCountDown;
    private Action<object> _onWeaponUpgrade;
    [SerializeField] protected AudioLibrarySounds soundEffect;

    protected virtual void OnEnable()
    {
        //Handle when a upgrade is chosen
        _onWeaponUpgrade = (param) => SetAttribute((WeaponData)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnWeaponUpgrade, _onWeaponUpgrade);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.OnWeaponUpgrade,_onWeaponUpgrade);
    }


    public void SetAttribute(WeaponData data)
    {
        Debug.Log("Set:"+type+",Fire Rate:"+data.Attribute.FireRate+",Power:"+data.Attribute.DamagePerAttack +",On object:"+gameObject.name);
        if (type == data.Type)
        {
            //Apply if correct type
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